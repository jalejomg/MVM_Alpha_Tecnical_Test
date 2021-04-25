using Alpha.Web.API.Constants;
using Alpha.Web.API.Domain.Models;
using Alpha.Web.API.Domain.Models.Validators;
using Xunit;

namespace Alpha.Tests.ModelValidators.Tests
{
    /// <summary>
    /// This class contain all unit tests about MessageModelValidator class 
    /// </summary>
    public class MessageModelValidatorTests
    {
        MessageModelValidator _validator;
        MessageModel _model;
        public MessageModelValidatorTests()
        {
            _model = new MessageModel();
            _validator = new MessageModelValidator();
        }

        [Fact]
        public void Validation_Success()
        {
            //Arrange
            _model.Body = "Body";
            _model.TypeCode = MessageTypes.InternalMessageCode;

            //Act
            var result = _validator.Validate(_model);

            //Asserts
            Assert.NotNull(result);
            Assert.True(result.IsValid);
            Assert.True(result.Errors.Count == 0);
        }

        [Fact]
        public void Validation_Fail_Body_Empty()
        {
            //Arrange
            _model.Body = "";
            _model.TypeCode = MessageTypes.InternalMessageCode;

            //Act
            var result = _validator.Validate(_model);

            //Asserts
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count == 1);
        }

        [Fact]
        public void Validation_Fail_TypeCode_Out_OF_Range()
        {
            //Arrange
            _model.Body = "Body";
            _model.TypeCode = 3;

            //Act
            var result = _validator.Validate(_model);

            //Asserts
            Assert.NotNull(result);
            Assert.False(result.IsValid);
            Assert.True(result.Errors.Count == 1);
        }
    }
}
