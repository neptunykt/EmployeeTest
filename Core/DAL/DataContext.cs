using Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Core.DAL
{
    public sealed class DataContext: DbContext
    {
        public DbSet<Employee> Employees { get; set; }
        
        public DataContext(DbContextOptions<DataContext> options) 
            :base(options)
        {
            Database.EnsureCreated();
        }
    }
}