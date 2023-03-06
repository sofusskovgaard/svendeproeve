namespace App.Services.Gateway.Common;

/// <summary>
/// Data required to create a department
/// </summary>
/// <param name="Name"></param>
/// <param name="Address"></param>
public record CreateDepartmentModel(string Name, string Address);

/// <summary>
/// Data required to update a department
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Address"></param>
public record UpdateDepartmentModel(string Name, string Address);