using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Serilog;

namespace Employee.Integration.Application.UseCases.Employees.Notifications
{
    public record EmployeeDeletedNotification(string Payroll_Number, string Forenames, string Telephone) : INotification;

    public class EmployeeDeletedNotificationHandler : INotificationHandler<EmployeeDeletedNotification>
    {
        public Task Handle(EmployeeDeletedNotification notification, CancellationToken cancellationToken)
        {
            Log.Information($"Employee Info Portal: Employee DELETED with Payroll Number : ' {notification.Payroll_Number} ', Forename is: ' {notification.Forenames} ' and Telephone is: ' {notification.Telephone} '.");

            return Task.CompletedTask;
        }
    }
}
