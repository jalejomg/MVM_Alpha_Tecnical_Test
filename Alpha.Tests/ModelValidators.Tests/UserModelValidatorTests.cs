using Alpha.Tests.Util;
using Alpha.Web.API.Domain.Models;
using Alpha.Web.API.Domain.Models.Validators;
using System.Threading.Tasks;
using Xunit;

namespace Alpha.Tests.ModelValidators.Tests
{
    /// <summary>
    /// This class contain all unit tests about UserModelValidator class
    /// </summary>
    public class UserModelValidatorTests
    {
        UserModelValidator _validator;
        UserModel _model;
        public UserModelValidatorTests()
        {
            _model = new UserModel();
            _validator = new UserModelValidator();
        }
        [Fact]
        public void Validation_Success()
        {
            //Arrange
            _model.Name = "Name";
            _model.Email = "email@alpha.co";

            //Act
            var result = _validator.Validate(_model);

            //Asserts
            Assert.NotNull(result);
            Assert.True(result.IsValid);
            Assert.True(result.Errors.Count == 0);
        }

        [Fact]
        public void Validation_Fail_Name_Empty()
        {
            //Arrange
            _model.Name = "";
            _model.Email = "email@alpha.co";

            //Act
            var result = _validator.Validate(_model);

            //Asserts
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count == 1);
        }

        [Fact]
        public void Validation_Fail_Name_More_Than_50_Characters()
        {
            //Arrange
            _model.Name = UtilRandomGenerator.GenerateString(51);
            _model.Email = "email@alpha.co";

            //Act
            var result = _validator.Validate(_model);

            //Asserts
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count == 1);
        }

        [Fact]
        public void Validation_Fail_Email_Empty()
        {
            //Arrange
            _model.Name = "Name";
            _model.Email = "";

            //Act
            var result = _validator.Validate(_model);

            //Asserts
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count == 1);
        }

        [Fact]
        public void Validation_Fail_Email_More_Than_50_Characters()
        {
            //Arrange
            _model.Name = "Name";
            _model.Email = UtilRandomGenerator.GenerateString(101);

            //Act
            var result = _validator.Validate(_model);

            //Asserts
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count == 1);
        }
    }
}
