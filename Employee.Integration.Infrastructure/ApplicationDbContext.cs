using System.Reflection;
using Employee.Integration.Application.Common.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Employee.Integration.Infrastructure
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public DbSet<Domain.Entities.Employee> Employees { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            base.OnModelCreating(modelBuilder);
        }
    }
}
