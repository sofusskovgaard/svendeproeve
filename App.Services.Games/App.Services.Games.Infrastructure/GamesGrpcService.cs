using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Games.Common.Dtos;
using App.Services.Games.Data.Entities;
using App.Services.Games.Infrastructure.Grpc;
using App.Services.Games.Infrastructure.Grpc.CommandMessages;
using App.Services.Games.Infrastructure.Grpc.CommandResults;
using AutoMapper;

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
    }
}