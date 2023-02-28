using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Turnaments.Common.Dtos;
using App.Services.Turnaments.Data.Entities;
using App.Services.Turnaments.Infrastructure.Grpc;
using App.Services.Turnaments.Infrastructure.Grpc.CommandMessages;
using App.Services.Turnaments.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using MongoDB.Driver;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Turnaments.Infrastructure
{
    public class TurnamentsGrpcService : BaseGrpcService, ITurnamentsGrpcService
    {
        private readonly IEntityDataService _entityDataService;
        private readonly IMapper _mapper;
        public TurnamentsGrpcService(IEntityDataService entityDataService, IMapper mapper)
        {
            _entityDataService = entityDataService;
            _mapper = mapper;
        }

        public ValueTask<GetAllTurnamentsCommandResult> GetAllTurnaments(GetAllTurnamentsCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var turnament = await this._entityDataService.ListEntities<TurnamentEntity>();

                return new GetAllTurnamentsCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    TurnamentDtos = this._mapper.Map<IEnumerable<TurnamentDto>>(turnament)
                };
            });
        }

        public ValueTask<CreateTurnamentCommandResult> CreateTurnament(CreateTurnamentCommandMessage message)
        {
            return TryAsync(async () =>
            {
                TurnamentEntity turnament = new TurnamentEntity
                {
                    Name = message.Name,
                    GameId = message.GameId,
                    MatchesId = message.MatchesId,
                    EventId = message.EventId
                };

                await this._entityDataService.Create<TurnamentEntity>(turnament);

                return new CreateTurnamentCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    }
                };
            });
        }
    }
}