namespace Twitter.Data
{
    using Microsoft.AspNet.Identity.EntityFramework;
    using Migrations;
    using Models;
    using System.Data.Entity;

    public class TwitterDbContext : IdentityDbContext<User>, ITwitterDbContext
    {
        public TwitterDbContext()
            : base("Context", throwIfV1Schema: false)
        {
            Database.SetInitializer( new MigrateDatabaseToLatestVersion<TwitterDbContext, Configuration>());
        }

        public virtual IDbSet<Tweet> Tweets { get; set; }
        public virtual IDbSet<Notification> Notifications { get; set; }

        public static TwitterDbContext Create()
        {
            return new TwitterDbContext();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasMany(u => u.Tweets)
                .WithRequired(t => t.User)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<User>()
                .HasMany(u => u.Followers)
                .WithMany(u => u.Following)
                .Map(x =>
                {
                    x.MapLeftKey("UserId");
                    x.MapRightKey("FollowerId");
                    x.ToTable("UserFollowers");
                });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Retweets)
                .WithMany(t => t.RetweetedBy)
                .Map(x =>
                {
                    x.MapLeftKey("UserId");
                    x.MapRightKey("TweetId");
                    x.ToTable("UserRetweets");
                });

            modelBuilder.Entity<User>()
               .HasMany(u => u.Favorites)
               .WithMany(t => t.FavoritedBy)
               .Map(x =>
               {
                   x.MapLeftKey("UserId");
                   x.MapRightKey("TweetId");
                   x.ToTable("UserFavorites");
               });

            modelBuilder.Entity<User>()
                .HasMany(u => u.Notifications)
                .WithRequired(n => n.CauseUser)
                .WillCascadeOnDelete(false);

            modelBuilder.Entity<Tweet>()
                .HasMany(t => t.Replies)
                .WithMany()
                .Map(x =>
                {
                    x.MapLeftKey("TweetId");
                    x.MapRightKey("ReplyTweetId");
                    x.ToTable("TweetReplies");
                });            

            base.OnModelCreating(modelBuilder);
        }
    }
}