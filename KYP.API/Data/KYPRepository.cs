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

        public async Task<Like> GetLike(int userId, int recipientId)
        {
            return await _dataContext.Likes
                .FirstOrDefaultAsync(u => u.LikerId == userId && 
                u.LikeeId == recipientId);
        }

        public async Task<Photo> GetMainPhoto(int userId)
        {
            var mainPhoto = await _dataContext.Photos
                .Where(u => u.UserId == userId)
                .FirstOrDefaultAsync(p => p.IsMain);
                
            return mainPhoto;
        }

        public async Task<Message> GetMessage(int messageId)
        {
            return await _dataContext.Messages
                .FirstOrDefaultAsync(m => m.Id == messageId);
        }

        public Task<PagedList<Message>> GetMessagesForUser()
        {
            throw new NotImplementedException();
        }

        public async Task<PagedList<Message>> GetMessagesForUser(MessageParams messageParams)
        {
            var messages = _dataContext.Messages
                .Include(u => u.Sender).ThenInclude(p => p.Photos)
                .Include(u => u.Recipient).ThenInclude(p => p.Photos)
                .AsQueryable();
            
            switch(messageParams.MessageContainer)
            {
                case "Inbox": 
                    messages = messages
                        .Where(u => u.RecipientId == messageParams.UserId);
                    break;
                
                case "Outbox":
                    messages = messages
                        .Where(u => u.SenderId == messageParams.UserId);
                    break;
                
                default:
                    messages = messages
                        .Where(u => u.RecipientId == messageParams.UserId
                            && u.IsRead == false);
                    break;
            }

            messages = messages.OrderByDescending(d => d.DateSent);

            return await PagedList<Message>.CreateAsync(messages, 
                messageParams.PageNumber, messageParams.PageSize);
        }

        public Task<IEnumerable<Message>> GetMessageThread(int userId, int recipientId)
        {
            throw new NotImplementedException();
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
            var users = _dataContext.Users.Include(p => p.Photos)
                                    .OrderByDescending(u => u.LastActive)
                                    .AsQueryable();

            users = users.Where(u => u.Id != userParams.UserId);

            users = users.Where(u => u.Gender == userParams.Gender);

            if (userParams.Likers)
            {
                var userLikers = await GetUserLikes(userParams.UserId,          
                    userParams.Likers);

                users = users.Where(u => userLikers.Contains(u.Id));
            }

            if (userParams.Likees)
            {
                var userLikees = await GetUserLikes(userParams.UserId,          
                    userParams.Likers);

                users = users.Where(u => userLikees.Contains(u.Id));
            }

            if (userParams.MinAge != 18 || userParams.MaxAge != 99)
            {
                var minDob = DateTime.Today.AddYears(-userParams.MaxAge - 1);
                var maxDob = DateTime.Today.AddYears(-userParams.MinAge);

                users = users.Where(u => u.DateOfBirth >= minDob 
                    && u.DateOfBirth <= maxDob);
            }

            if (!string.IsNullOrEmpty(userParams.OrderBy))
            {
                switch (userParams.OrderBy)
                {
                    case "created":
                        users = users.OrderByDescending(u => u.Created);
                        break;
                    default:
                        users = users.OrderByDescending(u => u.LastActive);
                        break;
                }
            }
            
            return await PagedList<User>.CreateAsync(users, 
                userParams.PageNumber, userParams.PageSize);
        }

        public async Task<bool> SaveAll()
        {
            return await _dataContext.SaveChangesAsync() > 0;
        }

        private async Task<IEnumerable<int>> GetUserLikes(int userId, bool likers)
        {
            var user = await _dataContext.Users
                .Include(x => x.Likers)
                .Include(x => x.Likees)
                .FirstOrDefaultAsync(u => u.Id == userId);
            
            if (likers)
            {
                return user.Likers
                    .Where(u => u.LikeeId == userId)
                    .Select(i => i.LikerId);
            } 
            else 
            {
                return user.Likees
                    .Where(u => u.LikerId == userId)
                    .Select(i => i.LikeeId);
            }
        }

        
    }
}