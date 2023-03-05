namespace App.Services.Gateway.Common;

/// <summary>
/// Data required to create a team
/// </summary>
/// <param name="Name"></param>
/// <param name="Bio"></param>
/// <param name="ProfilePicturePath"></param>
/// <param name="CoverPicturePath"></param>
/// <param name="GameId"></param>
/// <param name="MembersId"></param>
/// <param name="ManagerId"></param>
/// <param name="OrganizationId"></param>
public record CreateTeamModel(string Name, string Bio, string ProfilePicturePath, string CoverPicturePath, string GameId, string[] MembersId, string ManagerId, string OrganizationId);

/// <summary>
/// Data required to update a team
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Bio"></param>
/// <param name="ProfilePicturePath"></param>
/// <param name="CoverPicturePath"></param>
public record UpdateTeamModel(string Name, string Bio, string ProfilePicturePath, string CoverPicturePath);