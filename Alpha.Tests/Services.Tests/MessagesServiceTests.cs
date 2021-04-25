using Alpha.Web.API.Constants;
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
    /// <summary>
    /// This class contain all unit tests about MessageService class 
    /// </summary>
    public class MessagesServiceTests
    {
        private Message _messageEntity;
        private MessageModel _correctMessageModel;
        private MessageModel _incorrectMessageModel;
        private readonly MessagesService _service;
        private Mock<IMessagesRepository> _messageRepositoryMock;
        private MessageModelValidator _messageModelValidator;
        public MessagesServiceTests()
        {
            _messageEntity = new Message
            {
                Id = 123,
                Body = "message body",
                Type = MessageTypes.ExternalMessageCode,
                DeliveryStatus = DeliveryStates.DeliveredCode,
                State = EntityStatus.ExistsValue
            };

            _correctMessageModel = new MessageModel
            {
                Id = 123,
                Body = "message body",
                Type = MessageTypes.ExternalMessage,
                TypeCode = MessageTypes.ExternalMessageCode,
                DeliveryStatus = DeliveryStates.Delivered,
            };

            _incorrectMessageModel = new MessageModel();
            _messageRepositoryMock = new Mock<IMessagesRepository>();
            _messageModelValidator = new MessageModelValidator();
            _service = new MessagesService(_messageRepositoryMock.Object, _messageModelValidator);
        }

        [Fact]
        public async Task GetById_Return_MessageModel_With_Fields_Fill()
        {
            //Arrange
            _messageRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_messageEntity);

            //Act
            var result = await _service.GetByIdAsync(It.IsAny<int>());

            //Asserts
            Assert.NotNull(result);
            Assert.Equal(_messageEntity.Id, result.Id);
            Assert.Equal($"{MessagePrefixes.ExternalMessage}{_messageEntity.Id}", result.PublicId);
            Assert.Equal(_messageEntity.Body, result.Body);
            Assert.Equal(MessageTypes.ExternalMessage, result.Type);
            Assert.Equal(DeliveryStates.Delivered, result.DeliveryStatus);
        }

        [Fact]
        public async Task GetById_Throw_NotFoundCustomException_Entity_Null()
        {
            //Arrange          
            _messageRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Message)null);

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
            _messageEntity.State = EntityStatus.DeletedValue;

            _messageRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_messageEntity);

            //Act and Assert
            await Assert.ThrowsAsync<NotFoundCustomException>(async () =>
            {
                await _service.GetByIdAsync(It.IsAny<int>());
            });
        }

        [Fact]
        public async Task ListAsync_Returns_ResponseModel_OF_IEnumerable_OF_Message()
        {
            //Arrange
            var messagesList = new List<Message>();
            messagesList.Add(_messageEntity);

            _messageRepositoryMock.Setup(repository => repository.GetAllAsync())
                .ReturnsAsync(messagesList);

            //Act
            var result = await _service.ListAsync();

            //Asserts
            Assert.NotNull(result);
            Assert.True(messagesList[0].Id == result.Data.ElementAt(0).Id);
            Assert.True(messagesList[0].Body == result.Data.ElementAt(0).Body);
            Assert.True(MessageTypes.ExternalMessage == result.Data.ElementAt(0).Type);
            Assert.True(DeliveryStates.Delivered == result.Data.ElementAt(0).DeliveryStatus);
        }

        [Fact]
        public async Task CreateAsync_Returns_EntityId()
        {
            //Arrange
            Message createdMessageEntity = null;

            _messageRepositoryMock.Setup(repository => repository.CreateAsync(It.IsAny<Message>()))
                .ReturnsAsync(_messageEntity)
                .Callback<Message>(messageEntity =>
                    createdMessageEntity = messageEntity
                );

            //Act
            var result = await _service.CreateAsync(_correctMessageModel);
            _messageRepositoryMock.Verify(repository => repository.CreateAsync(It.IsAny<Message>()), Times.Once);

            //Asserts
            Assert.Equal(_messageEntity.Id, createdMessageEntity.Id);
            Assert.Equal(_messageEntity.Body, createdMessageEntity.Body);
            Assert.Equal(_messageEntity.Type, createdMessageEntity.Type);
            Assert.Equal(_messageEntity.DeliveryStatus, createdMessageEntity.DeliveryStatus);
        }

        [Fact]
        public async Task CreateAsync_Throw_ValidationException()
        {
            //Act and Assert
            await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await _service.CreateAsync(_incorrectMessageModel);
            });
        }

        [Fact]
        public async Task UpdateAsync_Returns_EntityId()
        {
            //Arrange
            Message updatedMessageEntity = null;

            _messageRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
               .ReturnsAsync(_messageEntity);

            _messageRepositoryMock.Setup(repository => repository.UpdateAsync(It.IsAny<Message>()))
                .ReturnsAsync(_messageEntity)
                .Callback<Message>(userEntity =>
                    updatedMessageEntity = userEntity
                );

            //Act
            var result = await _service.UpdateAsync(It.IsAny<int>(), _correctMessageModel);
            _messageRepositoryMock.Verify(repository => repository.UpdateAsync(It.IsAny<Message>()), Times.Once);

            //Asserts
            Assert.Equal(_messageEntity.Id, updatedMessageEntity.Id);
            Assert.Equal(_messageEntity.Body, updatedMessageEntity.Body);
            Assert.Equal(_messageEntity.Type, updatedMessageEntity.Type);
            Assert.Equal(_messageEntity.DeliveryStatus, updatedMessageEntity.DeliveryStatus);
        }

        [Fact]
        public async Task UpdateAsync_Throw_NotFoundCustomException()
        {
            //Arrange
            _messageRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
              .ReturnsAsync((Message)null);

            //Act and Assert
            await Assert.ThrowsAsync<NotFoundCustomException>(async () =>
            {
                await _service.UpdateAsync(It.IsAny<int>(), _correctMessageModel);
            });
        }

        [Fact]
        public async Task UpdateAsync_Throw_ValidationException()
        {
            //Act and Assert
            await Assert.ThrowsAsync<ValidationException>(async () =>
            {
                await _service.UpdateAsync(It.IsAny<int>(), _incorrectMessageModel);
            });
        }

        [Fact]
        public async Task Delete_Success()
        {
            //Arrange
            _messageRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_messageEntity);

            _messageRepositoryMock.Setup(repository => repository.UpdateAsync(It.IsAny<Message>()));

            //Act
            await _service.GetByIdAsync(It.IsAny<int>());
        }

        [Fact]
        public async Task Delete_Throw_NotFoundCustomException_Entity_Null()
        {
            //Arrange          
            _messageRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Message)null);

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
            _messageEntity.State = EntityStatus.DeletedValue;

            _messageRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_messageEntity);

            //Act and Assert
            await Assert.ThrowsAsync<NotFoundCustomException>(async () =>
            {
                await _service.DeleteAsync(It.IsAny<int>());
            });
        }

        [Fact]
        public async Task Send_Success()
        {
            //Arrange
            Message sentMessageEntity = null;

            _messageRepositoryMock.Setup(repository => repository.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(_messageEntity);

            _messageRepositoryMock.Setup(repository => repository.UpdateAsync(It.IsAny<Message>()))
               .ReturnsAsync(_messageEntity)
               .Callback<Message>(userEntity =>
                   sentMessageEntity = userEntity
               );

            //Act
            var result = await _service.UpdateAsync(It.IsAny<int>(), _correctMessageModel);
            _messageRepositoryMock.Verify(repository => repository.UpdateAsync(It.IsAny<Message>()), Times.Once);

            //Asserts
            Assert.Equal(_messageEntity.Id, sentMessageEntity.Id);
            Assert.Equal(_messageEntity.Body, sentMessageEntity.Body);
            Assert.Equal(_messageEntity.Type, sentMessageEntity.Type);
            Assert.Equal(_messageEntity.DeliveryStatus, sentMessageEntity.DeliveryStatus);
            Assert.Equal(DeliveryStates.DeliveredCode, sentMessageEntity.DeliveryStatus);
        }
    }
}
