using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class OrderManager : IOrderService
    {
        IOrderDal _orderDal;

        public OrderManager(IOrderDal orderDal)
        {
            _orderDal = orderDal;
        }

        public IResult Add(Order order)
        {
            throw new NotImplementedException();
        }

        public IResult Delete(Order order)
        {
            throw new NotImplementedException();
        }

        public IDataResult<List<Order>> GetAll()
        {
            throw new NotImplementedException();
        }

        public IDataResult<Order> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public IResult Update(Order order)
        {
            throw new NotImplementedException();
        }
    }
}
