using AccountValidation.Models;
using AccountValidationBL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace AccountValidator.Controllers
{
    public class ValidationController : ApiController
    {
        [HttpPost]
        public IHttpActionResult ValidateAccount([FromBody] AccountDetails accountDetails)
        {
            var bankAccountValidator = new BankAccountValidator();
            var validationResult = bankAccountValidator.ValidateBankAccount(accountDetails.SortCode, accountDetails.AccountNumber);

            var retVal = new ValidationResult() {IsValid = validationResult.IsValid, ValidationDetails = validationResult.ValidationDetails };
            return Ok(retVal);
        }

    }
}
