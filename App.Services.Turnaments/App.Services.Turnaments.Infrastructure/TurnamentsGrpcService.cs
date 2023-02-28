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

        #region Turnaments

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

        public ValueTask<UpdateTurnamentCommandResult> UpdateTurnament(UpdateTurnamentCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var turnament = this._mapper.Map<TurnamentEntity>(message.TurnamentDto);
                await this._entityDataService.Update<TurnamentEntity>(turnament);

                return new UpdateTurnamentCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    }
                };
            });
        }

        public ValueTask<DeleteTurnamentByIdCommandResult> DeleteTurnamentById(DeleteTurnamentByIdCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var turnament = await this._entityDataService.GetEntity<TurnamentEntity>(message.Id);

                GrpcCommandResultMetadata metadata;

                if (turnament != null)
                {
                    await this._entityDataService.Delete<TurnamentEntity>(turnament);

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
                        Message = "Could not find any Turnaments with that id"
                    };
                }

                return new DeleteTurnamentByIdCommandResult
                {
                    Metadata = metadata
                };
            });
        }
        #endregion

        #region Matches
        
        public ValueTask<GetMatchesByTurnamentIdCommandResult> GetMatchesByTurnamentId(GetMatchesByTurnamentIdCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var matches = await this._entityDataService.ListEntities(new ExpressionFilterDefinition<MatchEntity>(entity => entity.TurnamentId == message.TurnamentId));

                return new GetMatchesByTurnamentIdCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    MatchDtos = this._mapper.Map<IEnumerable<MatchDto>>(matches)
                };
            });
        }

        public ValueTask<GetMatchesByTeamIdCommandResult> GetMatchesByTeamId(GetMatchesByTeamIdCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var matches = await this._entityDataService.ListEntities(new ExpressionFilterDefinition<MatchEntity>(entity => entity.TeamsId.Contains(message.TeamId)));

                return new GetMatchesByTeamIdCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    MatchDtos = this._mapper.Map<IEnumerable<MatchDto>>(matches)
                };
            });
        }

        public ValueTask<GetMatchByIdCommandResult> GetMatchById(GetMatchByIdCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var match = await this._entityDataService.GetEntity<MatchEntity>(message.Id);

                return new GetMatchByIdCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    MatchDto = this._mapper.Map<MatchDto>(match)
                };
            });
        }

        public ValueTask<CreateMatchCommandResult> CreateMatch(CreateMatchCommandMessage message)
        {
            return TryAsync(async () =>
            {
                MatchEntity match = new MatchEntity
                {
                    Name = message.Name,
                    TeamsId = message.TeamsId,
                    TurnamentId = message.TurnamentId
                };

                match = await this._entityDataService.Create<MatchEntity>(match);

                return new CreateMatchCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    Id = match.Id
                };
            });
        }

        public ValueTask<UpdateMatchCommandResult> UpdateMatch(UpdateMatchCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var match = this._mapper.Map<MatchEntity>(message.MatchDto);
                await this._entityDataService.Update<MatchEntity>(match);

                return new UpdateMatchCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    }
                };
            });
        }
        
        #endregion
    }
}