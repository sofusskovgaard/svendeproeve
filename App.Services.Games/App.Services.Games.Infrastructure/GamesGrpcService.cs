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

namespace App.Services.Games.Infrastructure
{
    public class GamesGrpcService : BaseGrpcService, IGamesGrpcService
    {
        private readonly IEntityDataService _entityDataService;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        public GamesGrpcService(IEntityDataService entityDataService, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _entityDataService = entityDataService;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        public ValueTask<GetAllGamesGrpcCommandResult> GetAllGames(GetAllGamesGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var games = await this._entityDataService.ListEntities<GameEntity>();

                return new GetAllGamesGrpcCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    GameDtos = this._mapper.Map<IEnumerable<GameDto>>(games)
                };
            });
        }

        public ValueTask<GetGamesByNameGrpcCommandResult> GetGamesByName(GetGamesByNameGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var games = await this._entityDataService.ListEntities(new ExpressionFilterDefinition<GameEntity>(entity => entity.Name.Contains(message.Name)));

                return new GetGamesByNameGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    GameDtos = this._mapper.Map<IEnumerable<GameDto>>(games)
                };
            });
        }

        public ValueTask<GetGamesByGenreGrpcCommandResult> GetGamesByGenre(GetGamesByGenreGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var games = await this._entityDataService.ListEntities(new ExpressionFilterDefinition<GameEntity>(entity => entity.Genre.Contains(message.Genre)));

                return new GetGamesByGenreGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    GameDtos = this._mapper.Map<IEnumerable<GameDto>>(games)
                };
            });
        }

        public ValueTask<GetGameByIdGrpcCommandResult> GetGameById(GetGameByIdGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var game = await this._entityDataService.GetEntity<GameEntity>(message.Id);

                return new GetGameByIdGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    GameDto = this._mapper.Map<GameDto>(game)
                };
            });
        }

        public ValueTask<CreateGameGrpcCommandResult> CreateGame(CreateGameGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                await _publishEndpoint.Publish(new CreateGameCommandMessage
                {
                    Name = message.Name,
                    Discription = message.Discription,
                    ProfilePicture = message.ProfilePicture,
                    CoverPicture = message.CoverPicture,
                    Genre = message.Genre
                });

                return new CreateGameGrpcCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata()
                    {
                        Success = true
                    }
                };
            });
        }

        public ValueTask<UpdateGameGrpcCommandResult> updateGame(UpdateGameGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                await _publishEndpoint.Publish(new UpdateGameCommandMessage
                {
                    Id = message.GameDto.Id,
                    Name = message.GameDto.Name,
                    Discription = message.GameDto.Discription,
                    ProfilePicture = message.GameDto.ProfilePicture,
                    CoverPicture = message.GameDto.CoverPicture,
                    Genre = message.GameDto.Genre
                });

                return new UpdateGameGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    }
                };
            });
        }

        public ValueTask<DeleteGameByIdGrpcCommandResult> DeleteGameById(DeleteGameByIdGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var game = await this._entityDataService.GetEntity<GameEntity>(message.Id);

                GrpcCommandResultMetadata metadata;

                if (game != null)
                {
                    await this._entityDataService.Delete<GameEntity>(game);

                    metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    };
                }
                else
                {
                    metadata = new GrpcCommandResultMetadata
                    {
                        Success = false,
                        Message = "Could not find any games with that id"
                    };
                }

                return new DeleteGameByIdGrpcCommandResult
                {
                    Metadata = metadata
                };
            });
        }
    }
}