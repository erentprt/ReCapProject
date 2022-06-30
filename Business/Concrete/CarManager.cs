using System;
using System.Collections.Generic;
using System.Linq;
using Business.Abstract;
using Business.CCS;
using Business.Constants;
using Business.ValidationRules.FluentValidation;
using Core.Aspects.Autofac.Validation;
using Core.CrossCuttingConcerns.Validation;
using Core.Utilities.Business;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Concrete
{
    public class CarManager : ICarService
    {
        ICarDal _carDal;
        private ILogger _logger;
        private IColorService _colorService;

        public CarManager(ICarDal carDal, ILogger logger, IColorService colorService)
        {
            _carDal = carDal;
            _logger = logger;
            _colorService = colorService;
        }

        public IDataResult<List<Car>> GetAll()
        {
            return new DataResult<List<Car>>(_carDal.GetAll(), true, "Ürünler listelendi");
        }

        public IDataResult<Car> GetCarById(int id)
        {
            return new SuccessDataResult<Car>(_carDal.Get(c => c.Id == id));
        }

        public IDataResult<List<Car>> GetCarsByBrandId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetCarsByBrandId(c => c.BrandId == id).ToList());
        }

        public IDataResult<List<Car>> GetCarsByColorId(int id)
        {
            return new SuccessDataResult<List<Car>>(_carDal.GetCarsByBrandId(c => c.ColorId == id).ToList());
        }

        public IDataResult<List<CarDetailDto>> GetCarDetails()
        {
            return new SuccessDataResult<List<CarDetailDto>>(_carDal.GetCarDetails());
        }


        [ValidationAspect(typeof(CarValidator))]
        public IResult Add(Car car)
        {
            //business codes
            IResult result = BusinessRules.Run(CheckBrandItemValue(car.BrandId), CheckCarDescription(car.Description),CheckColorValue(car.ColorId));

            if (result != null)
            {
                return result;
            }

            return new ErrorResult();
        }

        [ValidationAspect(typeof(CarValidator))]
        public IResult Update(Car car)
        {
            _carDal.Update(car);
            return new SuccessResult(Messages.CarUpdated);
        }

        public IResult Delete(Car car)
        {
            _carDal.Delete(car);
            return new SuccessResult(Messages.CarDeleted);
        }

        private IResult CheckBrandItemValue(int brandId)
        {
            var result = _carDal.GetAll(c => c.BrandId == brandId).Count;
            if (result >= 10)
            {
                return new ErrorResult(Messages.CarCountOfCategoryError);
            }

            return new SuccessResult();
        }

        private IResult CheckCarDescription(string carDesc)
        {
            var result = _carDal.GetAll(c => c.Description == carDesc).Any();
            if (result)
            {
                return new ErrorResult(Messages.CarDescriptionAlreadyExists);
            }

            return new SuccessResult();
        }

        private IResult CheckColorValue(int colorId)
        {
            var result = _colorService.GetAll();
            if (result.Data.Count >= 15)
            {
                return new ErrorResult(Messages.CarDescriptionAlreadyExists);
            }
            return new SuccessResult();
        }
    }
}