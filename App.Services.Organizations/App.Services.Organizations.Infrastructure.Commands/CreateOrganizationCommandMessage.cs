﻿using App.Infrastructure.Commands;

namespace App.Services.Organizations.Infrastructure.Commands;

public class CreateOrganizationCommandMessage : ICommandMessage
{
    public string Name { get; set; }

    public string? Bio { get; set; }

    public string? ProfilePicture { get; set; }

    public string? CoverPicture { get; set; }

    public string? Address { get; set; }

    public string? DepartmentId { get; set; }
}