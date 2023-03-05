namespace App.Services.Gateway.Common;

/// <summary>
/// Data required to create a new turnament
/// </summary>
/// <param name="Name"></param>
/// <param name="GameId"></param>
/// <param name="EventId"></param>
public record CreateTournamentModel(string Name, string GameId, string EventId);

/// <summary>
/// Data required to update a turnament
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="GameId"></param>
public record UpdateTournamentModel(string Name, string GameId);

/// <summary>
/// Data required to create a new match
/// </summary>
/// <param name="Name"></param>
/// <param name="TeamsId"></param>
/// <param name="TurnamentId"></param>
public record CreateMatchModel(string Name, string[] TeamsId, string TurnamentId);

/// <summary>
/// Data required to update a match
/// </summary>
/// <param name="Id"></param>
/// <param name="Name"></param>
/// <param name="WinningTeamId"></param>
public record UpdateMatchModel(string Name, string WinningTeamId);