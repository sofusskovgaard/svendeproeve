using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Games.Common.Dtos;
using App.Services.Games.Data.Entities;
using App.Services.Games.Infrastructure.Grpc;
using App.Services.Games.Infrastructure.Grpc.CommandMessages;
using App.Services.Games.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using MongoDB.Driver;

namespace App.Services.Games.Infrastructure
{
    public class GamesGrpcService : BaseGrpcService, IGamesGrpcService
    {
        private readonly IEntityDataService _entityDataService;
        private readonly IMapper _mapper;
        public GamesGrpcService(IEntityDataService entityDataService, IMapper mapper)
        {
            _entityDataService = entityDataService;
            _mapper = mapper;
        }

        public ValueTask<GetAllGamesCommandResult> GetAllGames(GetAllGamesCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var games = await this._entityDataService.ListEntities<GameEntity>();

                return new GetAllGamesCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    GameDtos = this._mapper.Map<IEnumerable<GameDto>>(games)
                };
            });
        }

        public ValueTask<GetGamesByNameCommandResult> GetGamesByName(GetGamesByNameCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var games = await this._entityDataService.ListEntities(new ExpressionFilterDefinition<GameEntity>(entity => entity.Name.Contains(message.Name)));

                return new GetGamesByNameCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    GameDtos = this._mapper.Map<IEnumerable<GameDto>>(games)
                };
            });
        }

        public ValueTask<GetGamesByGenreCommandResult> GetGamesByGenre(GetGamesByGenreCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var games = await this._entityDataService.ListEntities(new ExpressionFilterDefinition<GameEntity>(entity => entity.Genre.Contains(message.Genre)));

                return new GetGamesByGenreCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    GameDtos = this._mapper.Map<IEnumerable<GameDto>>(games)
                };
            });
        }

        public ValueTask<GetGameByIdCommandResult> GetGameById(GetGameByIdCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var game = await this._entityDataService.GetEntity<GameEntity>(message.Id);

                return new GetGameByIdCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    GameDto = this._mapper.Map<GameDto>(game)
                };
            });
        }

        public ValueTask<CreateGameCommandResult> CreateGame(CreateGameCommandMessage message)
        {
            return TryAsync(async () =>
            {
                GameEntity game = new GameEntity
                {
                    Name = message.Name,
                    Discription = message.Discription,
                    ProfilePicture = message.ProfilePicture,
                    CoverPicture = message.CoverPicture,
                    Genre = message.Genre
                };

                await this._entityDataService.Create<GameEntity>(game);

                return new CreateGameCommandResult()
                {
                    Metadata = new GrpcCommandResultMetadata()
                    {
                        Success = true
                    }
                };
            });
        }

        public ValueTask<DeleteGameByIdCommandResult> DeleteGameById(DeleteGameByIdCommandMessage message)
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

                return new DeleteGameByIdCommandResult
                {
                    Metadata = metadata
                };
            });
        }
    }
}