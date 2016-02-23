using System.Data.Entity;
using Twitter.Models;

namespace Twitter.Data
{
    public interface ITwitterDbContext
    {
        IDbSet<Tweet> Tweets { get; set; }
        IDbSet<Notification> Notifications { get; set; }
    }
}
