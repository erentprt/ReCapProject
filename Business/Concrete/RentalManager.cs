using System.Collections.Generic;
using Business.Abstract;
using Core.Utilities.Results;
using DataAccess.Abstract;
using Entities.Concrete;

namespace Business.Concrete
{
    public class RentalManager : IRentalService
    {
        private IRentalDal _rentalDal;

        public RentalManager(IRentalDal rentalDal)
        {
            _rentalDal = rentalDal;
        }


        public IDataResult<List<Rental>> GetAll()
        {
            return new SuccessDataResult<List<Rental>>(_rentalDal.GetAll());
        }

        public IResult Add(Rental rental)
        {
            var result = CheckCar(rental);
            if (!result.Success)
            {
                return result;
            }
            _rentalDal.Add(rental);
            return new SuccessResult("Araba başarıyla eklendi");
        }

        public IResult Update(Rental rental)
        {
            _rentalDal.Update(rental);
            return new SuccessResult("Güncelleme işlemi başarılı");
        }

        public IResult Delete(Rental rental)
        {
            _rentalDal.Delete(rental);
            return new SuccessResult("Silme işlemi başarrılı");
        }

        public IResult CheckCar(Rental rental)
        {
            var result = _rentalDal.Get(r => r.CarId == rental.CarId && r.ReturnDate == null);
            if (result!=null)
            {
                if (rental.ReturnDate ==null || rental.ReturnDate>result.RentDate)
                {
                    return new ErrorResult("Araba daha önceden kiralanmıs");
                }
            }
            return new SuccessResult();
        }
        
    }
}