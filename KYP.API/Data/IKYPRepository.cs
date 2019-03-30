using System.Collections.Generic;
using System.Threading.Tasks;
using KYP.API.Helpers;
using KYP.API.Models;

namespace KYP.API.Data
{
    public interface IKYPRepository
    {
         void Add<T>(T entity) where T:class;
         void Delete<T>(T entity) where T:class;
         Task<bool> SaveAll();
         Task<PagedList<User>> GetUsers(UserParams userParams);
         Task<User> GetUser(int userId);
         Task<Photo> GetPhoto(int photoId);
         Task<Photo> GetMainPhoto(int userId);
         Task<Like> GetLike(int userId, int recipientId);
         Task<Message> GetMessage(int messageId);
         Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams);
         Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId);
    }
}