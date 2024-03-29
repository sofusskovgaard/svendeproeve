﻿using App.Data;
using App.Data.Attributes;

namespace App.Services.Authentication.Data.Entities;

[IndexDefinition("userid")]
[IndexDefinition("token_hash", isUnique: true)]
[CollectionDefinition(nameof(UserSessionEntity))]
public class UserSessionEntity : BaseEntity
{
    [IndexedProperty("userid")]
    public string UserId { get; set; }

    [IndexedProperty("token_hash")]
    public string TokenHash { get; set; }

    public string? IP { get; set; } = null;

    public string? UserAgent { get; set; } = null;

    public bool Active { get; set; } = true;
}