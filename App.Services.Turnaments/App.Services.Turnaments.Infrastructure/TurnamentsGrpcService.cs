using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Turnaments.Common.Dtos;
using App.Services.Turnaments.Data.Entities;
using App.Services.Turnaments.Infrastructure.Commands;
using App.Services.Turnaments.Infrastructure.Grpc;
using App.Services.Turnaments.Infrastructure.Grpc.CommandMessages;
using App.Services.Turnaments.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using MassTransit;
using MongoDB.Driver;
using ProtoBuf.Grpc.Configuration;

namespace App.Services.Turnaments.Infrastructure
{
    public class TurnamentsGrpcService : BaseGrpcService, ITurnamentsGrpcService
    {
        private readonly IEntityDataService _entityDataService;
        private readonly IMapper _mapper;
        private readonly IPublishEndpoint _publishEndpoint;
        public TurnamentsGrpcService(IEntityDataService entityDataService, IMapper mapper, IPublishEndpoint publishEndpoint)
        {
            _entityDataService = entityDataService;
            _mapper = mapper;
            _publishEndpoint = publishEndpoint;
        }

        #region Turnaments

        public ValueTask<GetAllTurnamentsGrpcCommandResult> GetAllTurnaments(GetAllTurnamentsGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var turnaments = await this._entityDataService.ListEntities<TurnamentEntity>();

                return new GetAllTurnamentsGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    TurnamentDtos = this._mapper.Map<IEnumerable<TurnamentDto>>(turnaments)
                };
            });
        }

        public ValueTask<GetTurnamentsByEventIdGrpcCommandResult> GetTurnamentsByEventId(GetTurnamentsByEventIdGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var turnaments = await this._entityDataService.ListEntities(new ExpressionFilterDefinition<TurnamentEntity>(entity => entity.EventId == message.EventId));

                return new GetTurnamentsByEventIdGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    TurnamentDtos = this._mapper.Map<IEnumerable<TurnamentDto>>(turnaments)
                };
            });
        }

        public ValueTask<GetTurnamentsByGameIdGrpcCommandResult> GetTurnamentsByGameId(GetTurnamentsByGameIdGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var turnaments = await this._entityDataService.ListEntities(new ExpressionFilterDefinition<TurnamentEntity>(entity => entity.GameId == message.GameId));

                return new GetTurnamentsByGameIdGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    TurnamentDtos = this._mapper.Map<IEnumerable<TurnamentDto>>(turnaments)
                };
            });
        }

        public ValueTask<GetTurnamentByMatchIdGrpcCommandResult> GetTurnamentByMatchId(GetTurnamentByMatchIdGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var turnament = await this._entityDataService.ListEntities(new ExpressionFilterDefinition<TurnamentEntity>(entity => entity.MatchesId.Contains(message.MatchId)));

                return new GetTurnamentByMatchIdGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    TurnamentDto = this._mapper.Map<TurnamentDto>(turnament.FirstOrDefault())
                };
            });
        }

        public ValueTask<GetTurnamentByIdGrpcCommandResult> GetTuenamentById(GetTurnamentByIdGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var turnament = await this._entityDataService.GetEntity<TurnamentEntity>(message.Id);

                return new GetTurnamentByIdGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    TurnamentDto = this._mapper.Map<TurnamentDto>(turnament)
                };
            });
        }

        public ValueTask<CreateTurnamentGrpcCommandResult> CreateTurnament(CreateTurnamentGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                await _publishEndpoint.Publish(new CreateTurnamentCommandMessage
                {

                    Name = message.Name,
                    GameId = message.GameId,
                    EventId = message.EventId
                });

                return new CreateTurnamentGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    }
                };
            });
        }

        public ValueTask<UpdateTurnamentGrpcCommandResult> UpdateTurnament(UpdateTurnamentGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                await _publishEndpoint.Publish(new UpdateTurnamentCommandMessage
                {
                    Id = message.TurnamentDto.Id,
                    Name = message.TurnamentDto.Name,
                    GameId = message.TurnamentDto.GameId,
                });

                return new UpdateTurnamentGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    }
                };
            });
        }

        public ValueTask<DeleteTurnamentByIdGrpcCommandResult> DeleteTurnamentById(DeleteTurnamentByIdGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                await _publishEndpoint.Publish(new DeleteTurnamentCommandMessage
                {
                    Id = message.Id
                });

                return new DeleteTurnamentByIdGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    }
                };
            });
        }
        #endregion

        #region Matches
        
        public ValueTask<GetMatchesByTurnamentIdGrpcCommandResult> GetMatchesByTurnamentId(GetMatchesByTurnamentIdGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var matches = await this._entityDataService.ListEntities(new ExpressionFilterDefinition<MatchEntity>(entity => entity.TurnamentId == message.TurnamentId));

                return new GetMatchesByTurnamentIdGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    MatchDtos = this._mapper.Map<IEnumerable<MatchDto>>(matches)
                };
            });
        }

        public ValueTask<GetMatchesByTeamIdGrpcCommandResult> GetMatchesByTeamId(GetMatchesByTeamIdGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var matches = await this._entityDataService.ListEntities(new ExpressionFilterDefinition<MatchEntity>(entity => entity.TeamsId.Contains(message.TeamId)));

                return new GetMatchesByTeamIdGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    MatchDtos = this._mapper.Map<IEnumerable<MatchDto>>(matches)
                };
            });
        }

        public ValueTask<GetMatchByIdGrpcCommandResult> GetMatchById(GetMatchByIdGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                var match = await this._entityDataService.GetEntity<MatchEntity>(message.Id);

                return new GetMatchByIdGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    },
                    MatchDto = this._mapper.Map<MatchDto>(match)
                };
            });
        }

        public ValueTask<CreateMatchGrpcCommandResult> CreateMatch(CreateMatchGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                await _publishEndpoint.Publish(new CreateMatchCommandMessage
                {
                    Name = message.Name,
                    TeamsId = message.TeamsId,
                    TurnamentId = message.TurnamentId
                });

                return new CreateMatchGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    }
                };
            });
        }

        public ValueTask<UpdateMatchGrpcCommandResult> UpdateMatch(UpdateMatchGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                await _publishEndpoint.Publish(new UpdateMatchCommandMessage
                {
                    Id = message.MatchDto.Id,
                    Name = message.MatchDto.Name,
                    WinningTeamId = message.MatchDto.WinningTeamId
                });

                return new UpdateMatchGrpcCommandResult
                {
                    Metadata = new GrpcCommandResultMetadata
                    {
                        Success = true
                    }
                };
            });
        }

        public ValueTask<DeleteMatchByIdGrpcCommandResult> DeleteMatchById(DeleteMatchByIdGrpcCommandMessage message)
        {
            return TryAsync(async () =>
            {
                await _publishEndpoint.Publish(new DeleteMatchCommandMessage
                {
                    Id = message.Id
                });

                return new DeleteMatchByIdGrpcCommandResult
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