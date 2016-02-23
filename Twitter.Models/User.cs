using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Twitter.Models
{
    public class User : IdentityUser
    {
        private ICollection<User> followers;
        private ICollection<User> following;
        private ICollection<Tweet> tweets;
        private ICollection<Tweet> retweets;
        private ICollection<Tweet> favorites;
        private ICollection<Notification> notifications;

        public User()
        {
            this.followers = new HashSet<User>();
            this.following = new HashSet<User>();
            this.tweets = new HashSet<Tweet>();
            this.retweets = new HashSet<Tweet>();
            this.favorites = new HashSet<Tweet>();
            this.notifications = new HashSet<Notification>();
        }

        public string PictureUrl { get; set; }

        public virtual ICollection<User> Followers { get; set; }
        public virtual ICollection<User> Following { get; set; }
        public virtual ICollection<Tweet> Tweets { get; set; }
        public virtual ICollection<Tweet> Retweets { get; set; }
        public virtual ICollection<Tweet> Favorites { get; set; }
        public virtual ICollection<Notification> Notifications { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<User> manager)
        {
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);

            return userIdentity;
        }
    }
}
