using Core.Entities;
using Core.Utilities.Results;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace Core.DataAccess.EntityFramework
{
    public class EfEntityRepositoryBase<TEntity, TContext> : IEntityRepository<TEntity>
        where TEntity : class, IEntity, new()
        where TContext : DbContext, new()
    {
        public IResult Add(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var addedEntity = context.Entry(entity);
                addedEntity.State = EntityState.Added;

                int result = context.SaveChanges();
                if (result == 0)
                {
                    return new ErrorResult("Add() işlemi gerçekleştirilemedi");
                }
                return new SuccessResult("Add() işlemi başarıyla gerçekleştirildi.");
            }
        }

        public IResult Delete(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var deletedEntity = context.Entry(entity);
                deletedEntity.State = EntityState.Deleted;

                int result = context.SaveChanges();
                if (result == 0)
                {
                    return new ErrorResult("Delete() işlemi gerçekleştirilemedi !");
                }
                return new SuccessResult("Delete() işlemi başarıyla gerçekleştirildi.");
            }
        }

        public IDataResult<TEntity> Get(Expression<Func<TEntity, bool>> filter)
        {
            using (TContext context = new TContext())
            {
                var result = context.Set<TEntity>().SingleOrDefault(filter);
                if (result == null)
                {
                    return new ErrorDataResult<TEntity>("Get() işlemi gerçekletirilemedi !");
                }
                return new SuccessDataResult<TEntity>(result, "Get() işlemi başarıyla gerçekleştirildi.");
            }
        }

        public IDataResult<List<TEntity>> GetAll(Expression<Func<TEntity, bool>> filter = null)
        {
            using (TContext context = new TContext())
            {
                if (filter == null)
                {
                    var result = context.Set<TEntity>().ToList();
                    if (result.Count > 0)
                    {
                        return new SuccessDataResult<List<TEntity>>(result, "GetAll() işlemi başarıyla gerçekleştirildi.");
                    }
                    return new ErrorDataResult<List<TEntity>>("GetAll() işlemi gerçekleştirilemedi !");
                }
                else
                {
                    var result = context.Set<TEntity>().Where(filter).ToList();

                    if (result.Count > 0)
                    {
                        return new SuccessDataResult<List<TEntity>>(result, "GetAll(expression) işlemi başarıyla gerçekleştirildi.");
                    }
                    return new ErrorDataResult<List<TEntity>>("GetAll(expression) işlemi gerçekleştirilemedi !");
                }
            }
        }

        public IResult Update(TEntity entity)
        {
            using (TContext context = new TContext())
            {
                var updatedEntity = context.Entry(entity);
                updatedEntity.State = EntityState.Modified;

                int result = context.SaveChanges();
                if (result == 0)
                {
                    return new ErrorResult("Update() işlemi gerçekleştirilemedi !");
                }
                return new SuccessResult("Update() işlemi başarıyla gerçekleştirildi.");
            }
        }
    }
}
