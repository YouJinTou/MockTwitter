using Twitter.Data.Repositories.Interfaces;
using Twitter.Models;

namespace Twitter.Data.Repositories
{
    public class TweetRepository : GenericRepository<Tweet>, IRepository<Tweet>
    {
        public TweetRepository(TwitterDbContext context)
            : base(context)
        {
        }
    }
}
