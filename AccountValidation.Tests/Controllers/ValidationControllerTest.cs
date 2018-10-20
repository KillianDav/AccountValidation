using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Web.Helpers;
using AccountValidation.Models;
using AccountValidator.Controllers;
using Newtonsoft.Json;
using System.Web.Http.Results;
using AccountValidationBL;
namespace AccountValidation.Tests.Controllers
{
    [TestClass]
    public class ValidationControllerTest
    {
        [TestMethod]
        public void ValidateAccount()
        {
            var controller = new ValidationController();

            var accountDetails = new AccountDetails() { AccountNumber = "12345678", SortCode = "123456" };

            var actionResult = controller.ValidateAccount(accountDetails);
            var contentResult = actionResult as OkNegotiatedContentResult<ValidationResult>;
            var obj = contentResult.Content;

            Assert.AreEqual(false, obj.IsValid);
        }
    }
}
