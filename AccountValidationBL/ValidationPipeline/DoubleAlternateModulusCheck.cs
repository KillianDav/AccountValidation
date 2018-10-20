using System;
using System.Collections.Generic;
using System.Text;
using AccountValidationBL;

namespace AccountValidationService.ValidationPipeline
{
    internal class DoubleAlternateModulusCheck : ModulusCheck
    {
        internal DoubleAlternateModulusCheck(BankAccountDetails bankAccountDetails, ModulusWeighting modulusWeighting) : base(bankAccountDetails, modulusWeighting)
        {
        }

        internal override int GetModulus()
        {
            throw new NotImplementedException();
        }

        internal override int GetSum()
        {
            throw new NotImplementedException();
        }
    }
}
