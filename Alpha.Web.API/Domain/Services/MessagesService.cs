using Alpha.Web.API.Constants;
using Alpha.Web.API.CustomExceptions;
using Alpha.Web.API.Data.Repositories;
using Alpha.Web.API.Domain.Models;
using FluentValidation;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Alpha.Web.API.Domain.Services
{
    /// <summary>
    /// This class contains core logic actions about Messaging domain
    /// </summary>
    public class MessagesService : IMessagesService
    {
        private IMessagesRepository _messagesRepository;
        public IValidator<MessageModel> _messageModelValidator;
        public MessagesService(
            IMessagesRepository messagesRepository,
            IValidator<MessageModel> messageModelValidator)
        {
            _messagesRepository = messagesRepository;
            _messageModelValidator = messageModelValidator;
        }

        public async Task<MessageModel> GetByIdAsync(int messageId)
        {
            var messageEntity = await _messagesRepository.GetByIdAsync(messageId);

            if (messageEntity == null) throw new NotFoundCustomException("Message was not found");
            if (!messageEntity.State) throw new NotFoundCustomException("Message was not found");

            return MessageModel.MakeOne(messageEntity);
        }

        public async Task<ResponseModel<IEnumerable<MessageModel>>> ListAsync()
        {
            var messageEntity = await _messagesRepository.GetAllAsync();

            return new ResponseModel<IEnumerable<MessageModel>>
            {
                Count = messageEntity.Count(),
                Data = MessageModel.MakeMany(messageEntity.Where(messageEntity => messageEntity.State))
            };
        }

        public async Task<int> CreateAsync(MessageModel messageModel)
        {
            await _messageModelValidator.ValidateAndThrowAsync(messageModel);

            var messageEntity = MessageModel.FillUp(messageModel);

            messageEntity.State = EntityStatus.ExistsValue;

            await _messagesRepository.CreateAsync(messageEntity);

            return messageEntity.Id;
        }

        public async Task<int> UpdateAsync(int messageId, MessageModel messageModel)
        {
            await _messageModelValidator.ValidateAndThrowAsync(messageModel);

            var messageEntity = await _messagesRepository.GetByIdAsync(messageId);

            if (messageEntity == null) throw new NotFoundCustomException("Message was not found");

            messageEntity = MessageModel.FillUp(messageModel);

            await _messagesRepository.UpdateAsync(messageEntity);

            return messageEntity.Id;
        }

        public async Task DeleteAsync(int messageId)
        {
            var messageEntity = await _messagesRepository.GetByIdAsync(messageId);

            if (messageEntity == null) throw new NotFoundCustomException("Message was not found");
            if (!messageEntity.State) throw new NotFoundCustomException("Message was not found");

            messageEntity.State = EntityStatus.DeletedValue;

            await _messagesRepository.UpdateAsync(messageEntity);
        }

        public async Task SendAsync(int messageId)
        {
            var messageEntity = await _messagesRepository.GetByIdAsync(messageId);

            if (messageEntity == null) throw new NotFoundCustomException("Message was not found");
            if (!messageEntity.State) throw new NotFoundCustomException("Message was not found");

            messageEntity.DeliveryStatus = DeliveryStates.DeliveredCode;

            await _messagesRepository.UpdateAsync(messageEntity);
        }
    }
}
