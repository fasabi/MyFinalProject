using Business.Abstract;
using Business.BusinessAspects.Autofac;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Caching;
using Core.Aspects.Autofac.Performance;
using Core.Aspects.Autofac.Transaction;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Business.Concrete
{
    public class ProductManager : IProductService
    {
        IProductDal _productDal;
        ICategoryService _categoryService;

        public ProductManager(IProductDal productDal, ICategoryService categoryService)
        {
            _productDal = productDal;
            _categoryService = categoryService;
        }

        [SecuredOperation("product.add, admin")]
        [CacheRemoveAspect("IProductService.Get")]
        [ValidationAspect(typeof(ProductValidator))]
        public IResult Add(Product product)
        {
            IResult result = BusinessRules.Run(CheckIfProductNameExists(product.ProductName), CheckIfProductCountOfCategoryCorrect(product.CategoryId), CheckIfCategoryLimitExceded());

            if (result != null)
            {
                return result;
            }

            _productDal.Add(product);
            return new SuccessResult(Messages.ProductAdded);
        }

        [CacheRemoveAspect("IProductService.Get")]
        public IResult Delete(Product product)
        {
            _productDal.Delete(product);

            return new SuccessResult(Messages.ProductDeleted);
        }

        [SecuredOperation("product.list")]
        [CacheAspect]
        public IDataResult<List<Product>> GetAll()
        {
            // İş Kodları
            if (DateTime.Now.Hour != 14)
            {
                var result = _productDal.GetAll();
                if (result.Count > 0)
                {
                    return new SuccessDataResult<List<Product>>(result, Messages.ProductListed);
                }
                return new ErrorDataResult<List<Product>>(Messages.ProductNotListed);
            }
            return new ErrorDataResult<List<Product>>(Messages.MaintenanceTime);
        }

        public IDataResult<List<Product>> GetAllByCategoryId(int id)
        {
            var result = _productDal.GetAll(p => p.CategoryId == id);
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<Product>>(result, Messages.ProductListed);
            }
            return new ErrorDataResult<List<Product>>(Messages.ProductNotListed);
        }

        [PerformanceAspect(1)]
        public IDataResult<Product> GetById(int id)
        {
            var product = _productDal.Get(p => p.ProductID == id);

            if (product != null)
            {
                return new SuccessDataResult<Product>(product, Messages.ProductListed);
            }
            return new ErrorDataResult<Product>(Messages.ProductNotListed);
        }

        public IDataResult<List<Product>> GetByUnitPrice(decimal min, decimal max)
        {
            var result = _productDal.GetAll(p => p.UnitPrice >= min && p.UnitPrice <= max);
            if (result.Count > 0)
            {
                return new SuccessDataResult<List<Product>>(result, Messages.GetByUnitPriceListed);
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

        [CacheRemoveAspect("IProductService.Get")]
        public IResult Update(Product product)
        {
            _productDal.Update(product);

            return new SuccessResult(Messages.ProductUpdated);
        }

        private IResult CheckIfProductCountOfCategoryCorrect(int categoryId)
        {
            //Select count(*) from products where categoryId=1
            var result = _productDal.GetAll(p => p.CategoryId == categoryId).Count;
            if (result >= 15)
            {
                return new ErrorResult(Messages.ProductCountOfCategoryError);
            }
            return new SuccessResult();
        }

        private IResult CheckIfProductNameExists(string productName)
        {
            var result = _productDal.GetAll(p => p.ProductName == productName).Any();
            if (result)
            {
                return new ErrorResult(Messages.ProductNameAlreadyExists);
            }
            return new SuccessResult();
        }

        private IResult CheckIfCategoryLimitExceded()
        {
            var result = _categoryService.GetAll();
            if (result.Data.Count > 15)
            {
                return new ErrorResult(Messages.CategoryLimitExceded);
            }

            return new SuccessResult();
        }

        [TransactionScopeAspect]
        public IResult AddTransactionalTest(Product product)
        {
            Add(product);

            if (product.UnitPrice < 15)
            {
                throw new Exception("Birim Fiyat 15'ten küçük olamaz !");
            }
            Add(product);
            return null;
        }
    }
}
