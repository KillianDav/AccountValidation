using System;
using System.Collections.Generic;
using System.Text;

namespace AccountValidationBL.ValidationPipeline
{
    internal abstract class ValidationStep
    {
        internal BankAccountValidationPipeline Pipeline { get; set; }
        internal BankAccountDetails BankAccountDetails { get; set; }
        internal ValidationStep NextStep { get; set; }
        internal bool IsValidated { get; set; }
        internal ValidationResult ValidationResult { get; set; }
        internal abstract void EvaluateStep();
    }
}
