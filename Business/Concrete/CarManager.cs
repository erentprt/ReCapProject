using System;
using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class CarManager:ICarService
    {
        ICarDal _carDal;

        public CarManager(ICarDal carDal)
        {
            _carDal = carDal;
        }

        public List<Car> GetAll()
        {
            return _carDal.GetAll();
        }

        public Car GetCarById(int id)
        {
            return _carDal.Get(c=>c.Id==id);
        }

        public List<Car> GetCarsByBrandId(int id)
        {
            return _carDal.GetCarsByBrandId(c=>c.BrandId==id).ToList();
        }

        public List<Car> GetCarsByColorId(int id)
        {
            return _carDal.GetCarsByBrandId(c=>c.ColorId==id).ToList();
        }

        public void Add(Car car)
        {
            if ((car.Description.Length >= 2) && (car.DailyPrice>0))
            {
                _carDal.Add(car);
            }
            else
            {
                throw new Exception("Hata");
            }
        }

        public void Update(Car car)
        {
            _carDal.Update(car);
        }

        public void Delete(Car car)
        {
            _carDal.Delete(car);
        }
    }
}