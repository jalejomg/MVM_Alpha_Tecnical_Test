using Alpha.Web.API.CustomExceptions;
using Alpha.Web.API.Data.Entities;
using Alpha.Web.API.Data.Repositories;
using Alpha.Web.API.Domain.Models;
using Alpha.Web.API.Domain.Models.Validators;
using Alpha.Web.API.Domain.Services;
using FluentValidation;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Alpha.Tests.Services.Tests
{
    public class UsersServiceTests
    {
        private User _userEntity;
        private UserModel _correctUserModel;
        private UserModel _incorrectUserModel;
        private readonly UsersService _service;
        private Mock<IUsersRepository> _userRepositoryMock;
        private UserModelValidator _userModelValidator;
        public UsersServiceTests()
        {
            _userEntity = new User
            {
                Id = 123,
                Name = "Alpha",
                LastName = "Communications",
                Email = "alpha.communications@alpha.co",
                State = true
            };

            _correctUserModel = new UserModel
            {
                Id = 123,
                Name = "Alpha",
                LastName = "Communications",
                Email = "alpha.communications@alpha.co"
            };

            _incorrectUserModel = new UserModel();
            _userRepositoryMock = new Mock<IUsersRepository>();
            _userModelValidator = new UserModelValidator();
            _service = new UsersService(_userRepositoryMock.Object, _userModelValidator);
        }

        [Fact]
        public async Task GetById_Return_UserModel_With_Fields_Fill()
        {
            //Arrange
            _userRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_userEntity);

            //Act
            var result = await _service.GetByIdAsync(It.IsAny<int>());

            //Asserts
            Assert.NotNull(result);
            Assert.Equal(_userEntity.Id, result.Id);
            Assert.Equal(_userEntity.Name, result.Name);
            Assert.Equal(_userEntity.LastName, result.LastName);
            Assert.Equal(_userEntity.Email, result.Email);
        }

        [Fact]
        public async Task GetById_Throw_NotFoundCustomException_Entity_Null()
        {
            //Arrange          
            _userRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((User)null);

            //Act and Assert
            await Assert.ThrowsAsync<NotFoundCustomException>(async () =>
            {
                await _service.GetByIdAsync(It.IsAny<int>());
            });
        }

        [Fact]
        public async Task GetById_Throw_NotFoundCustomException_Entity_State_False()
        {
            //Arrange
            _userEntity.State = false;

            _userRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_userEntity);

            //Act and Assert
            await Assert.ThrowsAsync<NotFoundCustomException>(async () =>
            {
                await _service.GetByIdAsync(It.IsAny<int>());
            });
        }

        [Fact]
        public async Task ListAsync_Returns_ResponseModel_OF_IEnumerable_OF_User()
        {
            //Arrange
            var usersList = new List<User>();
            usersList.Add(_userEntity);

            _userRepositoryMock.Setup(repository => repository.GetAllAsync())
                .ReturnsAsync(usersList);

            //Act
            var result = await _service.ListAsync();

            //Asserts
            Assert.NotNull(result);
            Assert.True(usersList[0].Id == result.Data.ElementAt(0).Id);
            Assert.True(usersList[0].Name == result.Data.ElementAt(0).Name);
            Assert.True(usersList[0].LastName == result.Data.ElementAt(0).LastName);
            Assert.True(usersList[0].Email == result.Data.ElementAt(0).Email);
        }

        [Fact]
        public async Task CreateAsync_Returns_EntityId()
        {
            //Arrange
            User createdUserEntity = null;

            _userRepositoryMock.Setup(repository => repository.CreateAsync(It.IsAny<User>()))
                .ReturnsAsync(_userEntity)
                .Callback<User>(userEntity =>
                    createdUserEntity = userEntity
                );

            //Act
            var result = await _service.CreateAsync(_correctUserModel);
            _userRepositoryMock.Verify(repository => repository.CreateAsync(It.IsAny<User>()), Times.Once);

            //Asserts
            Assert.Equal(_userEntity.Id, createdUserEntity.Id);
            Assert.Equal(_userEntity.Name, createdUserEntity.Name);
            Assert.Equal(_userEntity.LastName, createdUserEntity.LastName);
            Assert.Equal(_userEntity.Email, createdUserEntity.Email);
        }

        [Fact]
        public async Task CreateAsync_Throw_ValidationException()
        {
            //Act and Assert
            await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await _service.CreateAsync(_incorrectUserModel);
            });
        }

        [Fact]
        public async Task UpdateAsync_Returns_EntityId()
        {
            //Arrange
            User updatedUserEntity = null;

            _userRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(_userEntity);

            _userRepositoryMock.Setup(repository => repository.UpdateAsync(It.IsAny<User>()))
                .ReturnsAsync(_userEntity)
                .Callback<User>(userEntity =>
                    updatedUserEntity = userEntity
                );

            //Act
            var result = await _service.UpdateAsync(It.IsAny<int>(), _correctUserModel);
            _userRepositoryMock.Verify(repository => repository.UpdateAsync(It.IsAny<User>()), Times.Once);

            //Asserts
            Assert.Equal(_userEntity.Id, updatedUserEntity.Id);
            Assert.Equal(_userEntity.Name, updatedUserEntity.Name);
            Assert.Equal(_userEntity.LastName, updatedUserEntity.LastName);
            Assert.Equal(_userEntity.Email, updatedUserEntity.Email);
        }

        [Fact]
        public async Task UpdateAsync_Throw_NotFoundCustomException()
        {
            //Arrange
            _userRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
              .ReturnsAsync((User)null);

            //Act and Assert
            await Assert.ThrowsAsync<NotFoundCustomException>(async () =>
            {
                await _service.UpdateAsync(It.IsAny<int>(), _correctUserModel);
            });
        }

        [Fact]
        public async Task UpdateAsync_Throw_ValidationException()
        {
            //Act and Assert
            await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await _service.UpdateAsync(It.IsAny<int>(), _incorrectUserModel);
            });
        }

        [Fact]
        public async Task Delete_Success()
        {
            //Arrange
            _userRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_userEntity);

            _userRepositoryMock.Setup(repository => repository.UpdateAsync(It.IsAny<User>()));

            //Act
            await _service.GetByIdAsync(It.IsAny<int>());
        }

        [Fact]
        public async Task Delete_Throw_NotFoundCustomException_Entity_Null()
        {
            //Arrange          
            _userRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((User)null);

            //Act and Assert
            await Assert.ThrowsAsync<NotFoundCustomException>(async () =>
            {
                await _service.DeleteAsync(It.IsAny<int>());
            });
        }

        [Fact]
        public async Task Delete_Throw_NotFoundCustomException_Entity_State_False()
        {
            //Arrange
            _userEntity.State = false;

            _userRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_userEntity);

            //Act and Assert
            await Assert.ThrowsAsync<NotFoundCustomException>(async () =>
            {
                await _service.DeleteAsync(It.IsAny<int>());
            });
        }
    }
}
