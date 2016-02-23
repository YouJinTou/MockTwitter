using System.Data.Entity;
using System.Linq;
using Twitter.Data.Repositories.Interfaces;

namespace Twitter.Data.Repositories
{
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private DbContext context;
        private IDbSet<TEntity> set;

        public GenericRepository(DbContext context)
        {
            this.context = context;
            this.set = context.Set<TEntity>();
        }

        public IDbSet<TEntity> Set
        {
            get { return this.set; }
        }

        public IQueryable<TEntity> All()
        {
            return this.set;
        }

        public TEntity Find(object id)
        {
            return this.set.Find(id);
        }

        public void Add(TEntity entity)
        {
            this.ChangeState(entity, EntityState.Added);
        }

        public void Update(TEntity entity)
        {
            this.ChangeState(entity, EntityState.Modified);
        }

        public void Delete(TEntity entity)
        {
            this.ChangeState(entity, EntityState.Deleted);
        }

        public TEntity Delete(object id)
        {
            var entity = this.Find(id);
            this.Delete(entity);
            return entity;
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }

        private void ChangeState(TEntity entity, EntityState state)
        {
            var entry = this.context.Entry(entity);
            if (entry.State == EntityState.Detached)
            {
                this.set.Attach(entity);
            }

            entry.State = state;
        }
    }
}
