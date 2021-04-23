using Alpha.Web.API.Constants;
using Alpha.Web.API.Data.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Alpha.Web.API.Domain.Models
{
    public class MessageModel
    {
        public int Id { get; set; }
        public string PublicId { get; set; }
        public string Body { get; set; }
        public string Type { get; set; }
        public short TypeCode { get; set; }
        public string DeliveryStatus { get; set; }
        public User Addressee { get; set; }
        public User Sender { get; set; }

        public static MessageModel MakeOne(Message messageEntity)
        {
            return new MessageModel
            {
                Id = messageEntity.Id,
                PublicId = messageEntity.Type == MessageTypes.ExternalMessageCode ?
                    $"{MessagePrefixes.ExternalMessage}{messageEntity.Id}"
                    : $"{MessagePrefixes.InternalMessage}{messageEntity.Id}",
                Body = messageEntity.Body,
                DeliveryStatus = messageEntity.DeliveryStatus == DeliveryStates.DeliveredCode ?
                    DeliveryStates.Delivered
                    : DeliveryStates.Pending,
                Addressee = messageEntity.Addressee,
                Type = messageEntity.Type == MessageTypes.InternalMessageCode ?
                    MessageTypes.InternalMessage
                    : MessageTypes.ExternalMessage,
                TypeCode = messageEntity.Type,
                Sender = messageEntity.Sender
            };
        }

        public static IEnumerable<MessageModel> MakeMany(IEnumerable<Message> messageEntities)
        {
            return messageEntities.Select(messageEntity => MakeOne(messageEntity));
        }

        public static Message FillUp(MessageModel userModel)
        {
            return new Message
            {
                Id = userModel.Id,
                Body = userModel.Body,
                Type = userModel.Type == MessageTypes.ExternalMessage ?
                    MessageTypes.ExternalMessageCode
                    : MessageTypes.InternalMessageCode
            };
        }
    }
}
