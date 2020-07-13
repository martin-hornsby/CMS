using CMS.Users.WebApi.Requests.Users;
using CMS.Users.WebApi.Validators.UserController;
using NUnit.Framework;
using System.Linq;

namespace CMS.Users.WebApi.UnitTests.Validators.UserController
{
    [TestFixture]
    public class CreateUserValidatorTests
    {
        private readonly CreateUserValidator _validator = new CreateUserValidator();

        private CreateUser _request;

        [SetUp]
        public void Setup()
        {
            _request = new CreateUser { Username = "username" };
        }

        [Test]
        public void When_all_fields_are_valid_Then_validation_should_succeed()
        {
            var validationResult = _validator.Validate(_request);

            Assert.True(validationResult.IsValid);
        }

        [Test]
        public void When_Username_not_specified_Then_validation_should_fail()
        {
            _request.Username = null;

            var validationResult = _validator.Validate(_request);
            var error = validationResult.Errors.FirstOrDefault(q => q.PropertyName == "Username" && q.ErrorCode == "NotEmptyValidator");

            Assert.False(validationResult.IsValid);
            Assert.IsNotNull(error);
        }

        [Test]
        public void When_Username_is_empty_Then_validation_should_fail()
        {
            _request.Username = string.Empty;

            var validationResult = _validator.Validate(_request);
            var error = validationResult.Errors.FirstOrDefault(q => q.PropertyName == "Username" && q.ErrorCode == "NotEmptyValidator");

            Assert.False(validationResult.IsValid);
            Assert.IsNotNull(error);
        }

        [Test]
        public void When_Username_is_whitespace_Then_validation_should_fail()
        {
            _request.Username = " ";

            var validationResult = _validator.Validate(_request);
            var error = validationResult.Errors.FirstOrDefault(q => q.PropertyName == "Username" && q.ErrorCode == "NotEmptyValidator");

            Assert.False(validationResult.IsValid);
            Assert.IsNotNull(error);
        }

        [Test]
        public void When_Username_exceeds_maxlength_Then_validation_should_fail()
        {
            var maxLength = 50;
            _request.Username = new string(Enumerable.Repeat('a', maxLength + 1).ToArray());

            var validationResult = _validator.Validate(_request);
            var error = validationResult.Errors.FirstOrDefault(q => q.PropertyName == "Username" && q.ErrorCode == "MaximumLengthValidator");

            Assert.False(validationResult.IsValid);
            Assert.IsNotNull(error);
        }
    }
}
