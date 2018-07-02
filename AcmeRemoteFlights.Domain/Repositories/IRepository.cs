using AcmeRemoteFlights.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace AcmeRemoteFlights.Domain.Repositories
{
    public interface IRepository<TEntity> : IDisposable where TEntity : EntityBase
    {
        IEnumerable<TEntity> Get(
          Expression<Func<TEntity, bool>> filter = null,
          Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
          string includeProperties = "");

        TEntity GetByID(Guid entityID);
        void Insert(TEntity entity);
        void Delete(Guid entityID);
        void Update(TEntity entity);
       
    }
}
