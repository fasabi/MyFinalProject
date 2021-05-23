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
        public void Add(Product product)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var addedEntity = context.Entry(product);
                addedEntity.State = EntityState.Added;
                context.SaveChanges();
            }
        }

        public void Delete(Product product)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var deletedEntity = context.Entry(product);
                deletedEntity.State = EntityState.Deleted;
                context.SaveChanges();
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

        public List<ProductDetailDto> GetProductsDetails()
        {
            using (NorthwindContext context = new NorthwindContext())
            {

                var result = from p in context.Products
                             join c in context.Categories
                             on p.CategoryId equals c.CategoryId
                             select new ProductDetailDto
                             {
                                 ProductId = p.ProductID,
                                 ProductName = p.ProductName,
                                 CategoryName = c.CategoryName,
                                 UnitsInStock = p.UnitsInStock
                             };
                return result.ToList();
            }
        }

        public void Update(Product product)
        {
            using (NorthwindContext context = new NorthwindContext())
            {
                var updatedEntity = context.Entry(product);
                updatedEntity.State = EntityState.Modified;
                context.SaveChanges();
            }

        }
    }
}
