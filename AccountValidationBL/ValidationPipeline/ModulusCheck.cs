using AccountValidationBL;
using AccountValidationBL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountValidationService.ValidationPipeline
{
    internal abstract class ModulusCheck
    {
        internal ModulusCheck(BankAccountDetails bankAccountDetails,
                                      ModulusWeighting modulusWeighting)
        {
            this.BankAccountDetails = bankAccountDetails;
            this.ModulusWeighting = modulusWeighting;
        }
        
        internal BankAccountDetails BankAccountDetails { get; private set; }
        internal ModulusWeighting ModulusWeighting { get; private set; }
        internal virtual int ExpectedResult { get; set; }
        internal abstract int GetSum();
        internal abstract int GetModulus();

        internal bool IsValid()
        {
            return this.GetSum() % this.GetModulus() == this.ExpectedResult;
        }
        internal string ValidationDetails { get; set; }
    }
}
