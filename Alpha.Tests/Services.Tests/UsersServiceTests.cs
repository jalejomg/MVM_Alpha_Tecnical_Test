using Alpha.Web.API.Data.Entities;
using Alpha.Web.API.Data.Repositories;
using Alpha.Web.API.Domain.Models.Validators;
using Alpha.Web.API.Domain.Services;
using FluentValidation;
using Moq;
using System.Threading.Tasks;
using Xunit;

namespace Alpha.Tests.Services.Tests
{
    public class UsersServiceTests
    {
        private readonly UsersService _service;
        private Mock<IUsersRepository> _userRepositoryMock;
        private UserModelValidator _userModelValidator;
        public UsersServiceTests()
        {
            _userRepositoryMock = new Mock<IUsersRepository>();
            _userModelValidator = new UserModelValidator();
            _service = new UsersService(_userRepositoryMock.Object, _userModelValidator);
        }

        [Fact]
        public async Task GetById_Return_UserModel_With_Fields_Fill()
        {
            //Arrange
            var userEntity = new User
            {
                Id = 123,
                Name = "Alpha",
                LastName = "Communications",
                Email = "alpha.communications@alpha.co",
                State = true
            };

            _userRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(userEntity);

            //Act
            var result = await _service.GetByIdAsync(It.IsAny<int>());

            //Asserts
            Assert.NotNull(result);
            Assert.Equal(userEntity.Id, result.Id);
            Assert.Equal(userEntity.Name, result.Name);
            Assert.Equal(userEntity.LastName, result.LastName);
            Assert.Equal(userEntity.Email, result.Email);
        }
    }
}
