using App.Data.Services;
using App.Infrastructure.Commands;
using App.Services.Organizations.Data.Entities;
using App.Services.Organizations.Infrastructure.Commands;
using MassTransit;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace App.Services.Organizations.Infrastructure.CommandHandlers
{
    public class UpdateOrganizationCommandHandler : ICommandHandler<UpdateOrganizationCommandMessage>
    {
        private IEntityDataService _entityDataService;


        public UpdateOrganizationCommandHandler(IEntityDataService entityDataService)
        {
            _entityDataService = entityDataService;
        }
        public async Task Consume(ConsumeContext<UpdateOrganizationCommandMessage> context)
        {
            var message = context.Message;

            await _entityDataService.Update<OrganizationEntity>(
                filter => filter.Eq(entity => entity.Id, message.Id),
                builder => builder.Set(entity => entity.Name, message.Name)
                                  .Set(entity => entity.CoverPicture, message.CoverPicture)
                                  .Set(entity => entity.ProfilePicture, message.ProfilePicture)
                                  .Set(entity => entity.Address, message.Address)
                                  .Set(entity => entity.Bio, message.Bio)
                );
        }
    }
}
