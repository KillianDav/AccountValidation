using System;
using System.Collections.Generic;
using System.Text;

namespace AccountValidationBL.ValidationPipeline
{
    internal class EISCDSortCodeValidationStep : ValidationStep
    {
        internal EISCDSortCodeValidationStep(BankAccountDetails bankAccountDetails, BankAccountValidationPipeline pipeline)
        {
            this.BankAccountDetails = bankAccountDetails;
            this.Pipeline = pipeline;
        }
        internal override void EvaluateStep()
        {
            this.IsValidated = true;
            this.ValidationResult = new ValidationResult() { IsValid = true, ValidationDetails = "EISCDSortCodeValidation:N/A" };
            Pipeline.ValidationResult = this.ValidationResult;

            if (null != this.NextStep)
            {
                this.NextStep.EvaluateStep();
            }
        }
    }
}
