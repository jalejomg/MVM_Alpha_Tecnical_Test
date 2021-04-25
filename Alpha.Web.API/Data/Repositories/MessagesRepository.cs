using Alpha.Web.API.Data.Entities;

namespace Alpha.Web.API.Data.Repositories
{
    /// <summary>
    /// This class implement logic to transact data from Messages table
    /// </summary>
    public class MessagesRepository : GenericRepository<int, Message>, IMessagesRepository
    {
        public MessagesRepository(AlphaDbContext context) : base(context)
        {

        }
    }
}

