using System;
using System.Collections.Generic;
using System.Linq;
using DataAccess.Abstract;
using Entities.Concrete;

namespace DataAccess.Concrete.InMemory
{
    public class InMemoryCarDal:ICarDal
    {
        private List<Car> _cars;

        public InMemoryCarDal()
        {
            _cars = new List<Car>
            {
                new Car{Id = 1,Description = "Güsel araba",BrandId = 1,ColorId = 2,DailyPrice = 300,ModelYear = new DateTime(2003)},
                new Car{Id = 1,Description = "Ultra Mega Mükemel araba",BrandId = 4,ColorId = 1,DailyPrice = 700,ModelYear = new DateTime(2008)},
                new Car{Id = 1,Description = "Mükemel araba",BrandId = 3,ColorId = 1,DailyPrice = 500,ModelYear = new DateTime(2005)}
            };
        }

        public List<Car> GetAll()
        {
            return _cars;
        }

        public void Add(Car car)
        {
            _cars.Add(car);
        }

        public void Update(Car car)
        {
            Car carToUpdate = _cars.SingleOrDefault(c => c.Id == car.Id);
            carToUpdate.Description = car.Description;
            carToUpdate.BrandId = car.BrandId;
            carToUpdate.ColorId = car.ColorId;
            carToUpdate.DailyPrice = car.DailyPrice;
            carToUpdate.ModelYear = car.ModelYear;
        }

        public void Delete(Car car)
        {
            Car carToDelete = _cars.SingleOrDefault(c=>c.Id==car.Id);
            _cars.Remove(carToDelete);
        }

        public Car GetById(int id)
        {
            Car carById = _cars.SingleOrDefault(c => c.Id == id);
            return carById;
        }
    }
}