using App.Infrastructure.Commands;

namespace App.Services.Departments.Infrastructure.Commands;

public class UpdateDepartmentCommandMessage : ICommandMessage
{
    public string Id { get; set; }

    public string Name { get; set; }

    public string Address { get; set; }
}