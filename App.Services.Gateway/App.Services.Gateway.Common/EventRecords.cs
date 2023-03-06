namespace App.Services.Gateway.Common;

/// <summary>
/// Data required to create a new event
/// </summary>
/// <param name="EventName">Name of event</param>
/// <param name="Location">Location of event</param>
/// <param name="StartDate">Start date of the event</param>
/// <param name="EndDate">End date of the event</param>
public record CreateEventModel(string EventName, string Location, DateTime StartDate, DateTime EndDate);

public record UpdateEventModel(string Name, string Location, DateTime StartDate, DateTime EndDate);