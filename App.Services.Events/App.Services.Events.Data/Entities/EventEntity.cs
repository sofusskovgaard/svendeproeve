﻿using App.Data;
using App.Data.Attributes;

namespace App.Services.Events.Data.Entities;

[CollectionDefinition(nameof(EventEntity))]
public class EventEntity : BaseEntity
{
    public string EventName { get; set; }
    public string Location { get; set; }
    public string[] Tournaments { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime EndDate { get; set; }
}
