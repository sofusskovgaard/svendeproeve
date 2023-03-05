namespace App.Services.Gateway.Common;

public record CreateOrganizationModel(string Name, string Bio, string ProfilePicture, string CoverPicture, string Address, string DepartmentId);

public record UpdateOrganizaitonModel(string Name, string Bio, string ProfilePicture, string CoverPicture, string Address, string DepartmentId);