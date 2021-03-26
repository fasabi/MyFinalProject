using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryProductDal : IProductDal
    {
        List<Product> _products;
        public InMemoryProductDal()
        {
            _products = new List<Product> {
                new Product{ProductID=1, CategoryId=1, ProductName="Bardak", UnitInStock=15, UnitPrice=10},
                new Product{ProductID=2, CategoryId=1, ProductName="Kamera", UnitInStock=500, UnitPrice=3},
                new Product{ProductID=3, CategoryId=2, ProductName="Telefon", UnitInStock=1500, UnitPrice=2},
                new Product{ProductID=4, CategoryId=1, ProductName="Klavye", UnitInStock=150, UnitPrice=65},
                new Product{ProductID=5, CategoryId=1, ProductName="Fare", UnitInStock=85, UnitPrice=1}
            };
        }
        public void Add(Product product)
        {
            _products.Add(product);
        }

        public void Delete(Product product)
        {
            //Linq olmadan nasıl silme işlemi :

            //Product productToDelete = null;
            //foreach (var p in _products)
            //{
            //    if (product.ProductID == p.ProductID)
            //    {
            //        productToDelete = p;
            //    }
            //}

            Product productToDelete = _products.SingleOrDefault(p => p.ProductID == product.ProductID);

            _products.Remove(productToDelete);
        }

        public List<Product> GetAll()
        {
            return _products;
        }

        public List<Product> GetAllByCategory(int categoryId)
        {
            return _products.Where(p => p.CategoryId == categoryId).ToList();
        }

        public void Update(Product product)
        {
            Product productToUpdate = _products.SingleOrDefault(p => p.ProductID == product.ProductID);

            productToUpdate.ProductName = product.ProductName;
            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.UnitInStock = product.UnitInStock;
            productToUpdate.UnitPrice = product.UnitPrice;

        }
    }
}
