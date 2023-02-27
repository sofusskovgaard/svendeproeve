namespace App.Services.Authentication.Common.Enums;

public enum RoleTypes
{
    None = 0,
    Default = 1,
    SuperAdmin = Default << 1,
    SuperSuperAdmin = Default << 2,
}