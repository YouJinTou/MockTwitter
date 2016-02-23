using System.Linq;

namespace Twitter.Data.Repositories.Interfaces
{
    public interface IRepository<TEntity>
    {
        IQueryable<TEntity> All();

        TEntity Find(object id);

        void Add(TEntity entity);

        void Update(TEntity entity);

        void Delete(TEntity entity);

        TEntity Delete(object id);

        int SaveChanges();
    }
}
