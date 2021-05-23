using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;

        public ProductManager(IProductDal productDal)
        {
            _productDal = productDal;
        }

        public IResult Add(Product product)
        {
            if (product.ProductName.Length > 2)
            {
                var result = _productDal.Add(product);
                if (result.Success)
                {
                    return new ErrorResult(Messages.ProductAdded);
                }
                return new SuccessResult(Messages.ProductNotAdded);
            }
            return new ErrorResult(Messages.ProductNameInvalid);
        }

        public IResult Delete(Product product)
        {
            var result = _productDal.Delete(product);
            if (result.Success)
            {
                return new SuccessResult(Messages.ProductDeleted);
            }
            return new ErrorResult(Messages.ProductNotDeleted);
        }

        public IDataResult<List<Product>> GetAll()
        {
            // İş Kodları
            if (DateTime.Now.Hour != 14)
            {
                var result = _productDal.GetAll();
                if (result.Success)
                {
                    return new SuccessDataResult<List<Product>>(result.Data, Messages.ProductListed);
                }
                return new ErrorDataResult<List<Product>>(result.Message);
            }
            return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            var result = _productDal.GetAll(p => p.CategoryId == id);
            if (result.Success)
            {
                return new SuccessDataResult<List<Product>>(result.Data, Messages.ProductListed);
            }
            return new ErrorDataResult<List<Product>>(Messages.ProductNotListed);
        }

        public IDataResult<Product> GetById(int id)
        {
            var product = _productDal.Get(p => p.ProductID == id);

            if (product.Success)
            {
                return new SuccessDataResult<Product>(product.Data, Messages.ProductListed);
            }
            return new ErrorDataResult<Product>(Messages.ProductNotListed);
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            var result = _productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max);
            if (result.Success)
            {
                return new SuccessDataResult<List<Product>>(result.Data, Messages.GetByUnitPriceListed);
            }
            return new ErrorDataResult<List<Product>>(Messages.GetByUnitPriceNotListed);
        }

        public IDataResult<List<ProductDetailDto>> GetProductDetails()
        {
            if (DateTime.Now.Hour == 17)
            {
                return new ErrorDataResult<List<ProductDetailDto>>(Messages.MaintenanceTime);
            }
            return new SuccessDataResult<List<ProductDetailDto>>(_productDal.GetProductsDetails(), Messages.ProductListed);
        }

        public IResult Update(Product product)
        {
            var result = _productDal.Update(product);
            if (result.Success)
            {
                return new SuccessResult(Messages.ProductUpdated);
            }
            return new ErrorResult(Messages.ProductNotUpdated);
        }
    }
}
