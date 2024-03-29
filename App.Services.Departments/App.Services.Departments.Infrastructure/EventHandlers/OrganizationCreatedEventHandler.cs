﻿using App.Data.Services;
using App.Infrastructure.Events;
using App.Services.Departments.Data.Entities;
using App.Services.Organizations.Infrastructure.Events;
using MassTransit;
using Microsoft.IdentityModel.Tokens;
using MongoDB.Driver;

namespace App.Services.Departments.Infrastructure.EventHandlers;

public class OrganizationCreatedEventHandler : IEventHandler<OrganizationCreatedEventMessage>
{
    private readonly IEntityDataService _entityDataService;

    public OrganizationCreatedEventHandler(IEntityDataService entityDataService)
    {
        this._entityDataService = entityDataService;
    }

    public async Task Consume(ConsumeContext<OrganizationCreatedEventMessage> context)
    {
        var message = context.Message;

        var department = await this._entityDataService.GetEntity<DepartmentEntity>(message.DepartmentId);

        var organizations = new List<string>();
        if (!department.OrganizationIds.IsNullOrEmpty()) organizations = department.OrganizationIds.ToList();
        organizations.Add(message.Id);

        var updateDefinition =
            new UpdateDefinitionBuilder<DepartmentEntity>().Set(entity => entity.OrganizationIds,
                organizations.ToArray());

        await this._entityDataService.Update<DepartmentEntity>(
            filter => filter.Eq(entity => entity.Id, message.DepartmentId), _ => updateDefinition);
    }
}