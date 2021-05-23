using Business.Abstract;
using Business.Constants;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class CategoryManager : ICategoryService
    {
        ICategoryDal _categoryDal;

        public CategoryManager(ICategoryDal categoryDal)
        {
            _categoryDal = categoryDal;
        }

        public IResult Add(Category category)
        {
            var result = _categoryDal.Add(category);
            if (result.Success)
            {
                return new SuccessResult(Messages.CategoryAdded);
            }
            return new ErrorResult(Messages.CategoryNotAdded);
        }

        public IResult Delete(Category category)
        {
            var result = _categoryDal.Delete(category);
            if (result.Success)
            {
                return new SuccessResult(Messages.CategoryDeleted);
            }
            return new ErrorResult(Messages.CategoryNotDeleted);
        }

        public IResult Update(Category category)
        {
            var result = _categoryDal.Update(category);
            if (result.Success)
            {
                return new SuccessResult(Messages.CategoryUpdated);
            }
            return new ErrorResult(Messages.CategoryNotUpdated);
        }

        public IDataResult<List<Category>> GetAll()
        {
            var result = _categoryDal.GetAll();
            if (result.Success)
            {
                return new SuccessDataResult<List<Category>>(result.Data, Messages.CategoryListed);
            }
            return new ErrorDataResult<List<Category>>(Messages.CategoryNotListed);
        }

        public IDataResult<Category> GetById(int id)
        {
            var result = _categoryDal.Get(c => c.CategoryId == id);
            if (result.Success)
            {
                return new SuccessDataResult<Category>(result.Data, Messages.CategoryListed);
            }
            return new ErrorDataResult<Category>(Messages.CategoryNotListed);
        }
    }
}
