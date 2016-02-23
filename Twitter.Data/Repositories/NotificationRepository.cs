using Twitter.Data.Repositories.Interfaces;
using Twitter.Models;

namespace Twitter.Data.Repositories
{
    public class NotificationRepository : GenericRepository<Notification>, IRepository<Notification>
    {
        public NotificationRepository(TwitterDbContext context)
            : base(context)
        {
        }
    }
}
