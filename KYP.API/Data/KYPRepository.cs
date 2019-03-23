using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KYP.API.Helpers;
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

        public async Task<PagedList<User>> GetUsers(UserParams userParams)
        {
            var users = _dataContext.Users.Include(p => p.Photos).AsQueryable();

            users = users.Where(u => u.Id != userParams.UserId);

            users = users.Where(u => u.Gender == userParams.Gender);

            if (userParams.MinAge != 18 || userParams.MaxAge != 99)
            {
                var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

                users = users.Where(u => u.DateOfBirth >= minDob 
                    && u.DateOfBirth <= maxDob);
            }
            
            return await PagedList<User>.CreateAsync(users, 
                userParams.PageNumber, userParams.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }

        
    }
}