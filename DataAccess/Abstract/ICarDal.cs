using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Core.DataAccess;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Abstract
{
    public interface ICarDal:IEntityRepository<Car>
    {
        List<Car> GetCarsByBrandId(Expression<Func<Car, bool>> filter);
        List<Car> GetCarsByColorId(Expression<Func<Car, bool>> filter);
        List<CarDetailDto> GetCarDetails();

    }
}