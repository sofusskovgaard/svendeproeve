﻿using App.Infrastructure.Commands;

namespace App.Services.Turnaments.Infrastructure.Commands;

public class DeleteMatchCommandMessage : ICommandMessage
{
    public string Id { get; set; }
}