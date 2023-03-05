namespace App.Services.Gateway.Common;

/// <summary>
/// Data required to create a game
/// </summary>
/// <param name="Name"></param>
/// <param name="Description"></param>
/// <param name="ProfilePicture"></param>
/// <param name="CoverPicture"></param>
/// <param name="Genre"></param>
public record CreateGameModel(string Name, string Description, string ProfilePicture, string CoverPicture, string[] Genre);

/// <summary>
/// Data required to update a game
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="Description"></param>
/// <param name="ProfilePicture"></param>
/// <param name="CoverPicture"></param>
/// <param name="Genre"></param>
public record UpdateGameModel(string Name, string Description, string ProfilePicture, string CoverPicture, string[] Genre);