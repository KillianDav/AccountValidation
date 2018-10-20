using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AccountValidation.Models
{
    public class ValidationResult
    {
        public bool IsValid { get; set; }
        public string ValidationDetails { get; set; }
    }
}