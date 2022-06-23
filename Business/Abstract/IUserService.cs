using System.Collections.Generic;
using Core.Utilities.Results;
using Entities.Concrete;
using Entities.DTOs;

namespace Business.Abstract
{
    public interface IUserService
    {
        IDataResult<List<User>> GetAll();
        IDataResult<List<CarDetailDto>> GetUserDetails();
        IDataResult<User> GetUserById(int id);
        IResult Add(User user);
        IResult Update(User user);
        IResult Delete(User user);
    }
}