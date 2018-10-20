using System;
using System.Collections.Generic;
using System.Text;

namespace AccountValidationBL.ValidationPipeline
{
    internal class BankAccountValidationPipeline 
    {
        internal ValidationStep InitialValidationStep { get; set; }
        internal ValidationResult ValidationResult { get; set; }
        internal int NotImplementedException { get; set; }
        internal ValidationResult Process()
        {
            this.InitialValidationStep.EvaluateStep();
            return this.ValidationResult;
        }
    }
}
