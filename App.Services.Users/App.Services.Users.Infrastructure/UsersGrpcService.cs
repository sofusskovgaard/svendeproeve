using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Users.Data.Entities;
using ProtoBuf.Grpc.Configuration;
using App.Services.Users.Infrastructure.Grpc;
using App.Services.Users.Infrastructure.Grpc.CommandMessages;
using App.Services.Users.Infrastructure.Grpc.CommandResults;

namespace App.Services.Users.Infrastructure;

public class UsersGrpcService : IUsersGrpcService
{
    //private readonly IEntityDataService _entityDataService;

    //public UsersGrpcService(IEntityDataService entityDataService)
    //{
    //    this._entityDataService = entityDataService;
    //}

    public async Task<GetUserByIdCommandResult> GetUserById(GetUserByIdCommandMessage message)
    {
        //var entity = await this._entityDataService.GetEntity<UserEntity>(message.Id);

        return new GetUserByIdCommandResult()
        {
            Metadata = new GrpcCommandResultMetadata()
            {
                Success = true,
                Message = "Oh my lord it worked"
            }
        };
    }
}