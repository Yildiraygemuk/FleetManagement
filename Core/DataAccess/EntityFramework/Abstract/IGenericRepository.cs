using Core.Entities.Concrete;
using Core.Utilities.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework.Abstract
{
    public interface IGenericRepository<T> where T : BaseEntity, new()
    {
        IList<T> GetList(Expression<Func<T, bool>> filter = null);
        IQueryable<T> GetAllQueryable(Expression<Func<T, bool>> filter = null);
        T Get(Expression<Func<T, bool>> filter);
        T GetById(Guid id);
        IDataResult<T> Add(T entity);
        IDataResult<T> Update(T entity);
        IDataResult<List<T>> UpdateRange(List<T> entities);

    }
}
