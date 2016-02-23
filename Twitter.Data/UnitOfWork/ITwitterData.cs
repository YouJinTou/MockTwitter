using Twitter.Data.Repositories.Interfaces;
using Twitter.Models;

namespace Twitter.Data.UnitOfWork
{
    public interface ITwitterData
    {
        IRepository<User> Users { get; }
        IRepository<Tweet> Tweets { get; }
        IRepository<Notification> Notifications { get; }
        int Complete();
    }
}
