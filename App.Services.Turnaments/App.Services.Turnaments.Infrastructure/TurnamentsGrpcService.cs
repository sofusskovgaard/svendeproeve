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
                var turnaments = await this._entityDataService.ListEntities<TurnamentEntity>();

                return new GetAllTurnamentsCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    TurnamentDtos = this._mapper.Map<IEnumerable<TurnamentDto>>(turnaments)
                };
            });
        }

        public ValueTask<GetTurnamentsByEventIdCommandResult> GetTurnamentsByEventId(GetTurnamentsByEventIdCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var turnaments = await this._entityDataService.ListEntities(new ExpressionFilterDefinition<TurnamentEntity>(entity => entity.EventId == message.EventId));

                return new GetTurnamentsByEventIdCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    TurnamentDtos = this._mapper.Map<IEnumerable<TurnamentDto>>(turnaments)
                };
            });
        }

        public ValueTask<GetTurnamentsByGameIdCommandResult> GetTurnamentsByGameId(GetTurnamentsByGameIdCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var turnaments = await this._entityDataService.ListEntities(new ExpressionFilterDefinition<TurnamentEntity>(entity => entity.GameId == message.GameId));

                return new GetTurnamentsByGameIdCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    TurnamentDtos = this._mapper.Map<IEnumerable<TurnamentDto>>(turnaments)
                };
            });
        }

        public ValueTask<GetTurnamentByMatchIdCommandResult> GetTurnamentByMatchId(GetTurnamentByMatchIdCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var turnament = await this._entityDataService.ListEntities(new ExpressionFilterDefinition<TurnamentEntity>(entity => entity.MatchesId.Contains(message.MatchId)));

                return new GetTurnamentByMatchIdCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    TurnamentDto = this._mapper.Map<TurnamentDto>(turnament.FirstOrDefault())
                };
            });
        }

        public ValueTask<GetTurnamentByIdCommandResult> GetTuenamentById(GetTurnamentByIdCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var turnament = await this._entityDataService.GetEntity<TurnamentEntity>(message.Id);

                return new GetTurnamentByIdCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    TurnamentDto = this._mapper.Map<TurnamentDto>(turnament)
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