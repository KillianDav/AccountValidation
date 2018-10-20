using AccountValidationBL.ValidationPipeline;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountValidationService.ValidationPipeline
{
    internal class ModulusCheckValidationStep : ValidationStep
    {
        internal ModulusCheckValidationStep(ModulusCheck modulusCheck, BankAccountValidationPipeline pipeline)
        {
            this.ModulusCheck = modulusCheck;
            this.Pipeline = pipeline;
        }
        internal ModulusCheck ModulusCheck { get; set; }
        internal override void EvaluateStep()
        {
            if (this.ModulusCheck.IsValid())
            {
                this.IsValidated = true;
                this.ValidationResult = new ValidationResult() { IsValid = true, ValidationDetails = this.ModulusCheck.ValidationDetails };

                if (null != this.NextStep)
                {
                    this.NextStep.EvaluateStep();
                }
            }
            else
            {
                this.IsValidated = true;
                this.ValidationResult = new ValidationResult() { IsValid = false, ValidationDetails = this.ModulusCheck.ValidationDetails };
            }
            Pipeline.ValidationResult = this.ValidationResult;
        }
    }
}
