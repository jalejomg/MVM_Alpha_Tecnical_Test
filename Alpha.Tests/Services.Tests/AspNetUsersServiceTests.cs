using Alpha.Web.API.Constants;
using Alpha.Web.API.CustomExceptions;
using Alpha.Web.API.Data.Entities;
using Alpha.Web.API.Data.Repositories;
using Alpha.Web.API.Domain.Models;
using Alpha.Web.API.Domain.Models.Validators;
using Alpha.Web.API.Domain.Services;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Moq;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Alpha.Tests.Services.Tests
{
    /// <summary>
    /// This class contain all unit tests about UserService class 
    /// </summary>
    public class AspNetUsersServiceTests
    {
        private AspNetUser _userEntity;
        private UserModel _correctUserModel;
        private UserModel _incorrectUserModel;
        private readonly AspNetUsersService _service;
        private Mock<IAspNetUsersRepository> _userRepositoryMock;
        private UserModelValidator _userModelValidator;
        private static Mock<IUserStore<AspNetUser>> _userStoreMock;
        private static Mock<IRoleStore<AspNetRole>> _roleStoreMock;
        private static Mock<UserManager<AspNetUser>> _userManagerMock;
        private Mock<RoleManager<AspNetRole>> _roleManagerMock;
        public AspNetUsersServiceTests()
        {
            _userEntity = new AspNetUser
            {
                Id = "lsnfoisadfa",
                UserName = "Alpha",
                Email = "alpha.communications@alpha.co",
                State = EntityStatus.ExistsValue
            };

            _correctUserModel = new UserModel
            {
                Id = "lsnfoisadfa",
                Name = "Alpha",
                Email = "alpha.communications@alpha.co"
            };
            _userStoreMock = new Mock<IUserStore<AspNetUser>>();

            _roleStoreMock = new Mock<IRoleStore<AspNetRole>>();

            _userManagerMock = new Mock<UserManager<AspNetUser>>(_userStoreMock.Object,
                null, null, null, null, null, null, null, null);

            _roleManagerMock = new Mock<RoleManager<AspNetRole>>(_roleStoreMock.Object,
                null, null, null, null);

            _incorrectUserModel = new UserModel();
            _userRepositoryMock = new Mock<IAspNetUsersRepository>();
            _userModelValidator = new UserModelValidator();
            _service = new AspNetUsersService(
                _userRepositoryMock.Object,
                _userModelValidator,
                _userManagerMock.Object,
                _roleManagerMock.Object);
        }

        [Fact]
        public async Task GetByIdAsync_Return_UserModel_With_Fields_Fill()
        {
            //Arrange
            _userRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(_userEntity);

            //Act
            var result = await _service.GetByIdAsync(It.IsAny<string>());

            //Asserts
            Assert.NotNull(result);
            Assert.Equal(_userEntity.Id, result.Id);
            Assert.Equal(_userEntity.UserName, result.Name);
            Assert.Equal(_userEntity.Email, result.Email);
        }

        [Fact]
        public async Task GetByIdAsync_Throw_NotFoundCustomException_Entity_Null()
        {
            //Arrange          
            _userRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((AspNetUser)null);

            //Act and Assert
            await Assert.ThrowsAsync<NotFoundCustomException>(async () =>
            {
                await _service.GetByIdAsync(It.IsAny<string>());
            });
        }

        [Fact]
        public async Task GetByIdAsync_Throw_NotFoundCustomException_Entity_State_False()
        {
            //Arrange
            _userEntity.State = EntityStatus.DeletedValue;

            _userRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(_userEntity);

            //Act and Assert
            await Assert.ThrowsAsync<NotFoundCustomException>(async () =>
            {
                await _service.GetByIdAsync(It.IsAny<string>());
            });
        }

        [Fact]
        public async Task ListAsync_Returns_ResponseModel_OF_IEnumerable_OF_User()
        {
            //Arrange
            var usersList = new List<AspNetUser>();
            usersList.Add(_userEntity);

            _userRepositoryMock.Setup(repository => repository.GetAllAsync())
                .ReturnsAsync(usersList);

            //Act
            var result = await _service.ListAsync();

            //Asserts
            Assert.NotNull(result);
            Assert.True(usersList[0].Id == result.Data.ElementAt(0).Id);
            Assert.True(usersList[0].UserName == result.Data.ElementAt(0).Name);
            Assert.True(usersList[0].Email == result.Data.ElementAt(0).Email);
        }

        [Fact]
        public async Task AddAsync_Returns_EntityId()
        {
            //Arrange
            _userManagerMock.Setup(userManager => userManager.CreateAsync(It.IsAny<AspNetUser>(), It.IsAny<string>()))
                .ReturnsAsync(It.IsAny<IdentityResult>());

            //Act
            var result = await _service.AddAsync(_correctUserModel, It.IsAny<string>());

            _userManagerMock.Verify(userManager => userManager.CreateAsync(It.IsAny<AspNetUser>(), It.IsAny<string>())
                , Times.Once);
        }

        [Fact]
        public async Task CreateAsync_Throw_ValidationException()
        {
            //Act and Assert
            await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await _service.AddAsync(_incorrectUserModel, It.IsAny<string>());
            });
        }

        [Fact]
        public async Task UpdateAsync_Returns_EntityId()
        {
            //Arrange
            AspNetUser updatedUserEntity = null;

            _userRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<string>()))
               .ReturnsAsync(_userEntity);

            _userRepositoryMock.Setup(repository => repository.UpdateAsync(It.IsAny<AspNetUser>()))
                .ReturnsAsync(_userEntity)
                .Callback<AspNetUser>(userEntity =>
                    updatedUserEntity = userEntity
                );

            //Act
            var result = await _service.UpdateAsync(It.IsAny<string>(), _correctUserModel);
            _userRepositoryMock.Verify(repository => repository.UpdateAsync(It.IsAny<AspNetUser>()), Times.Once);

            //Asserts
            Assert.Equal(_userEntity.Id, updatedUserEntity.Id);
            Assert.Equal(_userEntity.UserName, updatedUserEntity.UserName);
            Assert.Equal(_userEntity.Email, updatedUserEntity.Email);
        }

        [Fact]
        public async Task UpdateAsync_Throw_NotFoundCustomException()
        {
            //Arrange
            _userRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<string>()))
              .ReturnsAsync((AspNetUser)null);

            //Act and Assert
            await Assert.ThrowsAsync<NotFoundCustomException>(async () =>
            {
                await _service.UpdateAsync(It.IsAny<string>(), _correctUserModel);
            });
        }

        [Fact]
        public async Task UpdateAsync_Throw_ValidationException()
        {
            //Act and Assert
            await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await _service.UpdateAsync(It.IsAny<string>(), _incorrectUserModel);
            });
        }

        [Fact]
        public async Task Delete_Success()
        {
            //Arrange
            _userRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(_userEntity);

            _userRepositoryMock.Setup(repository => repository.UpdateAsync(It.IsAny<AspNetUser>()));

            //Act
            await _service.GetByIdAsync(It.IsAny<string>());
        }

        [Fact]
        public async Task Delete_Throw_NotFoundCustomException_Entity_Null()
        {
            //Arrange          
            _userRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync((AspNetUser)null);

            //Act and Assert
            await Assert.ThrowsAsync<NotFoundCustomException>(async () =>
            {
                await _service.DeleteAsync(It.IsAny<string>());
            });
        }

        [Fact]
        public async Task Delete_Throw_NotFoundCustomException_Entity_State_False()
        {
            //Arrange
            _userEntity.State = EntityStatus.DeletedValue;

            _userRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<string>()))
                .ReturnsAsync(_userEntity);

            //Act and Assert
            await Assert.ThrowsAsync<NotFoundCustomException>(async () =>
            {
                await _service.DeleteAsync(It.IsAny<string>());
            });
        }
    }
}
