﻿using App.Infrastructure.Events;

namespace App.Services.Departments.Infrastructure.Events;

public class DepartmentDeletedEventMessage : IEventMessage
{
    public string Id { get; set; }
}