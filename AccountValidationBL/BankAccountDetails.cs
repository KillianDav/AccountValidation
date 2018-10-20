using System;

namespace AccountValidationBL
{
    internal class BankAccountDetails
    {
        internal BankAccountDetails(string sortCode, string accountNumber)
        {
            this.SortCode = double.Parse(sortCode);
            this.AccountNumber = accountNumber;
            this.ParseCombinedNumber(sortCode, accountNumber);
        }

        private void ParseCombinedNumber(string sortCode, string accountNumber)
        {
            string combinedNumber = sortCode + accountNumber;
            var digits = new int[14];
            for (int i = 0; i < 14; i++)
            {
                digits[i] = int.Parse(combinedNumber[i].ToString());
            }
            this.CombinedNumberDigits = digits;
        }
        
        internal double SortCode { get; set; }
        internal string AccountNumber { get; set; }
        internal int[] CombinedNumberDigits { get; private set; }

    }
}
