using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using EcommerceTakeHome.Core.Domain;

namespace EcommerceTakeHome.Core.Abstractions
{
    public interface IRepository<TEntity>
    {
        public IEnumerable<TEntity> Get(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = "");


        public TEntity GetByID(object id);


        public void Insert(TEntity entity);


        public void Delete(object id);


        public void Delete(TEntity entityToDelete);


        public void Update(TEntity entityToUpdate);

    }
}


