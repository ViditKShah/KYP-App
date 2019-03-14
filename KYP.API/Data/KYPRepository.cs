using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KYP.API.Models;
using Microsoft.EntityFrameworkCore;

namespace KYP.API.Data
{
    public class KYPRepository : IKYPRepository
    {
        private readonly DataContext _dataContext;
        public KYPRepository(DataContext context)
        {
            _dataContext = context;
        }

        public void Add<T>(T entity) where T : class
        {
            _dataContext.Add(entity);
        }

        public void Delete<T>(T entity) where T : class
        {
            _dataContext.Remove(entity);
        }

        public async Task<Photo> GetMainPhoto(int userId)
        {
            var mainPhoto = await _dataContext.Photos
                .Where(u => u.UserId == userId)
                .FirstOrDefaultAsync(p => p.IsMain);
            return mainPhoto;
        }

        public async Task<Photo> GetPhoto(int photoId)
        {
            var photo = await _dataContext.Photos
                .FirstOrDefaultAsync(p => p.Id == photoId);
            
            return photo;
        }

        public async Task<User> GetUser(int userId)
        {
            var user = await _dataContext.Users.Include(p => p.Photos)                  
                .FirstOrDefaultAsync(u => u.Id == userId);
            
            return user;
        }

        public async Task<IEnumerable<User>> GetUsers()
        {
            var users = await _dataContext.Users.Include(p => p.Photos).ToListAsync();
            return users;
        }

        public async Task<bool> SaveAll()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }

        
    }
}