using Microsoft.EntityFrameworkCore;

namespace Employee.Integration.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        public DbSet<Domain.Entities.Employee> Employees { get; set; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
