using KYP.API.Models;
using Microsoft.EntityFrameworkCore;

namespace KYP.API.Data
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options) {}

        public DbSet<Value> Values; 
    }
}