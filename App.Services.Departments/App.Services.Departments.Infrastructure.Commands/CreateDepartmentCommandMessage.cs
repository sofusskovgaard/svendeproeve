using App.Infrastructure.Commands;

namespace App.Services.Departments.Infrastructure.Commands
{
    public class CreateDepartmentCommandMessage : ICommandMessage
    {
        public string Name { get; set; }
        public string Address { get; set; }
    }
}
