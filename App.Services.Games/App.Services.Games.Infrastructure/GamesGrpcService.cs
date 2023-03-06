using App.Common.Grpc;
using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Games.Common.Dtos;
using App.Services.Games.Data.Entities;
using App.Services.Games.Infrastructure.Commands;
using App.Services.Games.Infrastructure.Grpc;
using App.Services.Games.Infrastructure.Grpc.CommandMessages;
using App.Services.Games.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Games.Infrastructure;

public class GamesGrpcService : BaseGrpcService, IGamesGrpcService
{
    private readonly IEntityDataService _entityDataService;

    private readonly IMapper _mapper;

    private readonly IPublishEndpoint _publishEndpoint;

    public GamesGrpcService(IEntityDataService entityDataService, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        this._entityDataService = entityDataService;
        this._mapper = mapper;
        this._publishEndpoint = publishEndpoint;
    }

    public ValueTask<GetAllGamesGrpcCommandResult> GetAllGames(GetAllGamesGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            var games = await this._entityDataService.ListEntities<GameEntity>();

            return new GetAllGamesGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata{ Success = true },
                Data = this._mapper.Map<IEnumerable<GameDto>>(games)
            };
        });
    }

    public ValueTask<GetGamesByNameGrpcCommandResult> GetGamesByName(GetGamesByNameGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            var games = await this._entityDataService.ListEntities<GameEntity>(filter => filter.Text(message.Name));

            return new GetGamesByNameGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata{ Success = true },
                Data = this._mapper.Map<IEnumerable<GameDto>>(games)
            };
        });
    }

    public ValueTask<GetGamesByGenreGrpcCommandResult> GetGamesByGenre(GetGamesByGenreGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            var games = await this._entityDataService.ListEntities<GameEntity>(filter => filter.Text(message.Genre));

            return new GetGamesByGenreGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata{ Success = true },
                Data = this._mapper.Map<IEnumerable<GameDto>>(games)
            };
        });
    }

    public ValueTask<GetGameByIdGrpcCommandResult> GetGameById(GetGameByIdGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            var game = await this._entityDataService.GetEntity<GameEntity>(message.Id);

            return new GetGameByIdGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata{ Success = true },
                Data = this._mapper.Map<GameDto>(game)
            };
        });
    }

    public ValueTask<CreateGameGrpcCommandResult> CreateGame(CreateGameGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            await this._publishEndpoint.Publish(new CreateGameCommandMessage
            {
                Name = message.Name,
                Discription = message.Description,
                ProfilePicture = message.ProfilePicture,
                CoverPicture = message.CoverPicture,
                Genre = message.Genre
            });

            return new CreateGameGrpcCommandResult{ Metadata = new GrpcCommandResultMetadata { Success = true } };
        });
    }

    public ValueTask<UpdateGameGrpcCommandResult> updateGame(UpdateGameGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            await this._publishEndpoint.Publish(new UpdateGameCommandMessage
            {
                Id = message.Id,
                Name = message.Name,
                Discription = message.Discription,
                ProfilePicture = message.ProfilePicture,
                CoverPicture = message.CoverPicture,
                Genre = message.Genre
            });

            return new UpdateGameGrpcCommandResult { Metadata = new GrpcCommandResultMetadata { Success = true } };
        });
    }

    public ValueTask<DeleteGameByIdGrpcCommandResult> DeleteGameById(DeleteGameByIdGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            await this._publishEndpoint.Publish(new DeleteGameCommandMessage { Id = message.Id });

            return new DeleteGameByIdGrpcCommandResult { Metadata = new GrpcCommandResultMetadata { Success = true } };
        });
    }
}