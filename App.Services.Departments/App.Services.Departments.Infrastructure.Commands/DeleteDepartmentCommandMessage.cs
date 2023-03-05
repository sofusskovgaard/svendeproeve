using App.Infrastructure.Commands;

namespace App.Services.Departments.Infrastructure.Commands
{
    public class DeleteDepartmentCommandMessage : ICommandMessage
    {
        public string Id { get; set; }
    }
}
