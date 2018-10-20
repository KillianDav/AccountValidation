using AccountValidationBL.ValidationPipeline;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using AccountValidationService.ValidationPipeline;

namespace AccountValidationBL
{
    public class BankAccountValidator
    {
        public ValidationResult ValidateBankAccount(string sortCode, string accountNumber)
        {
            // Sanity check inputs
            if (!(sortCode.Length == 6 && sortCode.All(c => c >= '0' && c <= '9')))
            {
                throw new ArgumentException("Invalid sort code");
            }
            // Sanity check inputs
            if (!(accountNumber.Length == 8 && accountNumber.All(c => c >= '0' && c <= '9')))
            {
                throw new ArgumentException("Invalid account number");
            }

            var pipeline = this.CreateValidationPipeline(new BankAccountDetails(sortCode, accountNumber));
            return pipeline.Process();

        }

        internal BankAccountValidationPipeline CreateValidationPipeline(BankAccountDetails bankAccountDetails)
        {

            var pipeline = new BankAccountValidationPipeline();

            // note - EISCDSortCodeValidationStep does not do anything.
            pipeline.InitialValidationStep = new EISCDSortCodeValidationStep(bankAccountDetails, pipeline);
            var currentValidationStep = pipeline.InitialValidationStep;

            var modulusWeightings = (new VocalinkModulusWeightingData().GetModulusWeightings());
            var weightingsForSortCode = modulusWeightings.Where(w => w.SortCodeStart <= bankAccountDetails.SortCode
                                                                && w.SortCodeEnd <= bankAccountDetails.SortCode);

            // Create validation pipeline steps according to weightings from Vocalink
            foreach (var weighting in weightingsForSortCode)
            {
                ModulusCheck modulusCheck = null;
                // Apply Execptions when creating pipeline
                // Exception 4 - adjust expected result
                if (4 == weighting.ExceptionNumber)
                {
                    modulusCheck = new Standard11ModulusCheck(bankAccountDetails, weighting);
                    modulusCheck.ExpectedResult = int.Parse(bankAccountDetails.CombinedNumberDigits[12].ToString() + bankAccountDetails.CombinedNumberDigits[12].ToString());
                }
                else
                {
                    switch (weighting.ModulusAlgorithmEnum)
                    {
                        case Enums.ModulusAlgorithmEnum.MOD10:
                            modulusCheck = new Standard10ModulusCheck(bankAccountDetails, weighting);
                            break;
                        case Enums.ModulusAlgorithmEnum.MOD11:
                            modulusCheck = new Standard11ModulusCheck(bankAccountDetails, weighting);
                            break;
                        case Enums.ModulusAlgorithmEnum.DBLAL:
                            modulusCheck = new DoubleAlternateModulusCheck(bankAccountDetails, weighting);
                            break;
                    }
                }
                // Excpetion 7 - if g=9 then zeroise u-b
                if (7 == weighting.ExceptionNumber)
                {
                    if (9 == bankAccountDetails.CombinedNumberDigits[12])
                    {
                        for (int i = 0; i < 8; i++)
                        {
                            bankAccountDetails.CombinedNumberDigits[i] = 0;
                        }
                    }
                }
                // Indicate Exceptions not applied
                if (weighting.ExceptionNumber.HasValue
                    && (!(weighting.ExceptionNumber == 4
                    || weighting.ExceptionNumber == 7))
                    )
                {
                    modulusCheck.ValidationDetails = string.Format("Excluded adjustment for Exception {0}", weighting.ExceptionNumber.ToString());
                }

                var validationStep = new ModulusCheckValidationStep(modulusCheck, pipeline);
                currentValidationStep.NextStep = validationStep;
                currentValidationStep = validationStep;
            }

            return pipeline;
        }
    }
}
