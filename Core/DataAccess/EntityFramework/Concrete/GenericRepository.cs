using Core.DataAccess.EntityFramework.Abstract;
using Core.Entities.Concrete;
using Core.Utilities.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.DataAccess.EntityFramework.Concrete
{
    public class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : BaseEntity, new()
    {
        protected readonly DbContext Context;
        protected readonly DbSet<TEntity> DbSet;
        public GenericRepository(DbContext context)
        {
            Context = context;
            DbSet = Context.Set<TEntity>();
        }
        public IList<TEntity> GetList(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null
               ? DbSet.ToList()
               : DbSet.Where(filter).ToList();
        }
        public IQueryable<TEntity> GetAllQueryable(Expression<Func<TEntity, bool>> filter = null)
        {
            return filter == null
                ? DbSet.AsQueryable().Where(x => x.IsActive)
                : DbSet.Where(filter).AsQueryable().Where(x => x.IsActive);
        }
        public TEntity Get(Expression<Func<TEntity, bool>> filter)
        {
            return DbSet.Where(x => x.IsActive).FirstOrDefault(filter);
        }
        public TEntity GetById(Guid id)
        {
            return DbSet.FirstOrDefault(x => x.Id == id && x.IsActive);
        }
        public IDataResult<TEntity> Add(TEntity entity)
        {
            entity.AddDate = DateTime.Now;
            var addedEntity = Context.Entry(entity);
            addedEntity.State = EntityState.Added;
            Context.SaveChanges();
            return new SuccessDataResult<TEntity>(addedEntity.Entity);
        }
        public IDataResult<TEntity> Update(TEntity entity)
        {
            entity.UpdateDate = DateTime.Now;
            var updatedEntity = Context.Entry(entity);
            updatedEntity.State = EntityState.Modified;
            Context.SaveChanges();
            return new SuccessDataResult<TEntity>(updatedEntity.Entity);
        }
        public IDataResult<List<TEntity>> UpdateRange(List<TEntity> entities)
        {
            entities.ForEach(x =>
            {
                x.UpdateDate = DateTime.Now;
            });

            DbSet.UpdateRange(entities);
            Context.SaveChanges();
            return new SuccessDataResult<List<TEntity>>(entities);
        }

    }
}
