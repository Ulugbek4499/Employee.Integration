using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MediatR;
using Serilog;

namespace Employee.Integration.Application.UseCases.Employees.Notifications
{
    public record EmployeeCreatedNotification(string Payroll_Number, string Forenames, string Telephone) : INotification;

    public class EmployeeCreatedLogNotificationHandler : INotificationHandler<EmployeeCreatedNotification>
    {
        public Task Handle(EmployeeCreatedNotification notification, CancellationToken cancellationToken)
        {
            Log.Information($"Employee Info Portal: New Employee CREATED with Payroll Number : ' {notification.Payroll_Number} ', Forename is: ' {notification.Forenames} ' and Telephone is: ' {notification.Telephone} '.");

            return Task.CompletedTask;
        }
    }

    public class EmployeeCreatedConsoleNotificationHandler : INotificationHandler<EmployeeCreatedNotification>
    {
        public async Task Handle(EmployeeCreatedNotification notification, CancellationToken cancellationToken)
        {
            Log.Information($"Employee Info Portal: New Employee CREATED with Payroll Number : ' {notification.Payroll_Number} ', Forename is: ' {notification.Forenames} ' and Telephone is: ' {notification.Telephone} '.");
        }
    }
}
