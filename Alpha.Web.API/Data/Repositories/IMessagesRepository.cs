using Alpha.Web.API.Data.Entities;

namespace Alpha.Web.API.Data.Repositories
{
    /// <summary>
    /// This class declare methods to transact data from Messages table
    /// </summary>
    public interface IMessagesRepository : IGenericRepository<int, Message>
    {
    }
}
