using Core.DataAccess;
using Core.Utilities.Results;
using DataAccess.Abstract;
using DataAccess.Concrete.EntityFramework;
using Entities.Concrete;
using Entities.DTOs;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        public InMemoryProductDal()
        {
        }
        public bool Add(Product product)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var addedEntity = context.Entry(product);
                addedEntity.State = EntityState.Added;

                int result = context.SaveChanges();
                if (result == 0)
                {
                    return false;
                }
                return true;
            }
        }

        public bool Delete(Product product)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var deletedEntity = context.Entry(product);
                deletedEntity.State = EntityState.Deleted;

                int result = context.SaveChanges();
                if (result == 0)
                {
                    return false;
                }
                return true;
            }
        }

        public Product Get(Expression<Func<Product, bool>> filter)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return context.Set<Product>().SingleOrDefault(filter);
            }
        }

        public List<Product> GetAll(Expression<Func<Product, bool>> filter = null)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return filter == null ? context.Set<Product>().ToList() : context.Set<Product>().Where(filter).ToList();
            }
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                return context.Set<Product>().Where(p => p.CategoryId == categoryId).ToList();
            }
        }


        public IDataResult<ProductDetailDto> GetProductDetails(int id)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<ProductDetailDto>> GetProductsDetails()
        {
            throw new NotImplementedException();
        }

        public bool Update(Product product)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var updatedEntity = context.Entry(product);
                updatedEntity.State = EntityState.Modified;

                int result = context.SaveChanges();
                if (result == 0)
                {
                    return false;
                }
                return true;
            }

        }

        IResult IEntityRepository<Product>.Add(Product entity)
        {
            throw new NotImplementedException();
        }

        IResult IEntityRepository<Product>.Delete(Product entity)
        {
            throw new NotImplementedException();
        }

        IDataResult<Product> IEntityRepository<Product>.Get(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        IDataResult<List<Product>> IEntityRepository<Product>.GetAll(Expression<Func<Product, bool>> filter)
        {
            throw new NotImplementedException();
        }

        List<ProductDetailDto> IProductDal.GetProductsDetails()
        {
            throw new NotImplementedException();
        }

        IResult IEntityRepository<Product>.Update(Product entity)
        {
            throw new NotImplementedException();
        }
    }
}
