using System.Collections.Generic;
using System.IO;
using System.Text;
using KYP.API.Models;
using Newtonsoft.Json;

namespace KYP.API.Data
{
    public class Seed
    {
        private readonly DataContext _dataContext;
        public Seed(DataContext context)
        {
            _dataContext = context;
        }

        public void SeedUsers() 
        {
            var userData = File.ReadAllText("Data/UserSeedData.json");
            var users = JsonConvert.DeserializeObject<List<User>>(userData);
            foreach (var user in users)
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash("password", out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
                user.UserName = user.UserName.ToLower();

                _dataContext.Users.Add(user);
            }

            _dataContext.SaveChanges();
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            using(var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(Encoding.UTF8.GetBytes(password));
            }
        }
    }
}