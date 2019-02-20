using System.Collections.Generic;
using System.Threading.Tasks;
using KYP.API.Models;

namespace KYP.API.Data
{
    public interface IKYPRepository
    {
         void Add<T>(T entity) where T:class;
         void Delete<T>(T entity) where T:class;
         Task<bool> SaveAll();
         Task<IEnumerable<User>> GetUsers();
         Task<User> GetUser(int userId);
    }
}