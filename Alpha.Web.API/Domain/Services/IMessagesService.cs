using Alpha.Web.API.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Alpha.Web.API.Domain.Services
{
    public interface IMessagesService
    {
        Task<MessageModel> GetByIdAsync(int messageId);
        Task<ResponseModel<IEnumerable<MessageModel>>> ListAsync();
        Task<int> CreateAsync(MessageModel messageModel);
        Task<int> UpdateAsync(int messageId, MessageModel messageModel);
        Task DeleteAsync(int messageId);
        Task SendAsync(int messageId);
    }
}
