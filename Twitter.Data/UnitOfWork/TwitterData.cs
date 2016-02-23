using System;
using System.Collections.Generic;
using System.Data.Entity;
using Twitter.Data.Repositories;
using Twitter.Data.Repositories.Interfaces;
using Twitter.Models;

namespace Twitter.Data.UnitOfWork
{
    public class TwitterData : ITwitterData
    {
        private readonly DbContext context;
        private IDictionary<Type, object> repositories;

        public TwitterData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }        

        public IRepository<Notification> Notifications
        {
            get { return this.GetRepository<Notification>(); }
        }        

        public IRepository<Tweet> Tweets
        {
            get { return this.GetRepository<Tweet>(); }
        }

        public IRepository<User> Users
        {
            get { return this.GetRepository<User>(); }
        }

        public int Complete()
        {
            return this.context.SaveChanges();
        }

        private IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (!this.repositories.ContainsKey(typeof(TEntity)))
            {
                var type = typeof(GenericRepository<TEntity>);
                this.repositories.Add(typeof(TEntity),
                    Activator.CreateInstance(type, this.context));
            }

            return (IRepository<TEntity>)this.repositories[typeof(TEntity)];
        }
    }
}
