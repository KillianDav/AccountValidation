using System;
using System.Collections.Generic;
using System.Text;
using AccountValidationBL;

namespace AccountValidationService.ValidationPipeline
{
    internal class Standard11ModulusCheck : ModulusCheck
    {
        internal Standard11ModulusCheck(BankAccountDetails bankAccountDetails, ModulusWeighting modulusWeighting) : base(bankAccountDetails, modulusWeighting)
        {
        }
        internal override int GetSum()
        {
            int sum = 0;
            for (var i = 0; i < 14; i++)
            {
                sum += BankAccountDetails.CombinedNumberDigits[i] * ModulusWeighting.Weights[i];
            }
            return sum;
        }
        internal override int GetModulus()
        {
            return 11;
        }
    }
}
