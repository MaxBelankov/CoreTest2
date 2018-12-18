using Microsoft.EntityFrameworkCore;
using CoreTest2.Common.Models;

namespace CoreTest2.DAL
{
    public class InMemoryDbContext : DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if(!optionsBuilder.IsConfigured)
                optionsBuilder.UseInMemoryDatabase("CoreTest2");
        }

        public DbSet<Employee> Employees { get; set; }
    }
}