using AccountValidationBL.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace AccountValidationBL
{
    internal class ModulusWeighting
    {
        public double SortCodeStart { get; set; }
        public double SortCodeEnd { get; set; }
        public ModulusAlgorithmEnum ModulusAlgorithmEnum { get; set; }
        public int[] Weights { get; set; }
        public int? ExceptionNumber { get; set; }
    }
}
