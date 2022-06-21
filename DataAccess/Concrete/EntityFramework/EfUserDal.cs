using System.Collections.Generic;
using System.Linq;
using Core.DataAccess.EntityFramework;
using DataAccess.Abstract;
using Entities.Concrete;
using Entities.DTOs;

namespace DataAccess.Concrete.EntityFramework
{
    public class EfUserDal:EfEntityRepositoryBase<User,ReCapProjectContext>,IUserDal
    {
        public List<UserDetailDto> GetUserDetails()
        {
            using (ReCapProjectContext context = new ReCapProjectContext())
            {
                var result = from u in context.Users
                    join c in context.Customers on u.Id equals c.UserId
                    select new UserDetailDto{
                        Id = u.Id,
                        FirstName = u.FirstName,
                        LastName = u.LastName,
                        CompanyName = c.CompanyName
                    };
                return result.ToList();
            }
        }
    }
}