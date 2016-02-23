using Twitter.Data.Repositories.Interfaces;
using Twitter.Models;

namespace Twitter.Data.Repositories
{
    public class UserRepository : GenericRepository<User>, IRepository<User>
    {
        public UserRepository(TwitterDbContext context)
            : base(context)
        {
        }
    }
}
