using System;
using System.Collections.Generic;
using System.Text;

namespace AccountValidationBL.ValidationPipeline
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string ValidationDetails { get; set; }
    }
}
