using Employee.Integration.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Employee.Integration.Application.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Employee> Employees { get; set; }
       
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
