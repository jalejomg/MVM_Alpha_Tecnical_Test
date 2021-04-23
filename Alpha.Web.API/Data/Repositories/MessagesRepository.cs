using Alpha.Web.API.Data.Entities;

namespace Alpha.Web.API.Data.Repositories
{
    public class MessagesRepository : GenericRepository<int, Message>, IMessagesRepository
    {
        public MessagesRepository(AlphaDbContext context) : base(context)
        {

        }
    }
}

