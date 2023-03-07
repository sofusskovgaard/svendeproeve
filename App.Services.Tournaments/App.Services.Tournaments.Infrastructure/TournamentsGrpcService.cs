using App.Common.Grpc;
using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Tournaments.Common.Dtos;
using App.Services.Tournaments.Data.Entities;
using App.Services.Tournaments.Infrastructure.Commands;
using App.Services.Tournaments.Infrastructure.Grpc;
using App.Services.Tournaments.Infrastructure.Grpc.CommandMessages;
using App.Services.Tournaments.Infrastructure.Grpc.CommandResults;
using AutoMapper;
using MassTransit;
using MongoDB.Driver;

namespace App.Services.Tournaments.Infrastructure;

public class TournamentsGrpcService : BaseGrpcService, ITournamentsGrpcService
{
    private readonly IEntityDataService _entityDataService;

    private readonly IMapper _mapper;

    private readonly IPublishEndpoint _publishEndpoint;

    public TournamentsGrpcService(IEntityDataService entityDataService, IMapper mapper, IPublishEndpoint publishEndpoint)
    {
        _entityDataService = entityDataService;
        _mapper = mapper;
        _publishEndpoint = publishEndpoint;
    }

    #region Tournaments

    public ValueTask<GetAllTournamentsGrpcCommandResult> GetAllTournaments(GetAllTournamentsGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var turnaments = await _entityDataService.ListEntities<TournamentEntity>();

            return new GetAllTournamentsGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = true
                },
                TurnamentDtos = _mapper.Map<IEnumerable<TournamentDto>>(turnaments)
            };
        });
    }

    public ValueTask<GetTournamentsByEventIdGrpcCommandResult> GetTournamentsByEventId(
        GetTournamentsByEventIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var turnaments = await _entityDataService.ListEntities(
                new ExpressionFilterDefinition<TournamentEntity>(entity => entity.EventId == message.EventId));

            return new GetTournamentsByEventIdGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = true
                },
                TurnamentDtos = _mapper.Map<IEnumerable<TournamentDto>>(turnaments)
            };
        });
    }

    public ValueTask<GetTournamentsByGameIdGrpcCommandResult> GetTournamentsByGameId(
        GetTournamentsByGameIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var turnaments = await _entityDataService.ListEntities(
                new ExpressionFilterDefinition<TournamentEntity>(entity => entity.GameId == message.GameId));

            return new GetTournamentsByGameIdGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = true
                },
                TurnamentDtos = _mapper.Map<IEnumerable<TournamentDto>>(turnaments)
            };
        });
    }

    public ValueTask<GetTournamentByMatchIdGrpcCommandResult> GetTournamentByMatchId(
        GetTournamentByMatchIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var turnament = await _entityDataService.ListEntities(
                new ExpressionFilterDefinition<TournamentEntity>(entity => entity.MatchesId.Contains(message.MatchId)));

            return new GetTournamentByMatchIdGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = true
                },
                TournamentDto = _mapper.Map<TournamentDto>(turnament.FirstOrDefault())
            };
        });
    }

    public ValueTask<GetTournamentByIdGrpcCommandResult> GetTournamentById(GetTournamentByIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var turnament = await _entityDataService.GetEntity<TournamentEntity>(message.Id);

            return new GetTournamentByIdGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = true
                },
                TournamentDto = _mapper.Map<TournamentDto>(turnament)
            };
        });
    }

    public ValueTask<CreateTournamentGrpcCommandResult> CreateTournament(CreateTournamentGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            await _publishEndpoint.Publish(new CreateTournamentCommandMessage
            {
                Name = message.Name,
                GameId = message.GameId,
                EventId = message.EventId
            });

            return new CreateTournamentGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = true
                }
            };
        });
    }

    public ValueTask<UpdateTournamentGrpcCommandResult> UpdateTournament(UpdateTournamentGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            await _publishEndpoint.Publish(new UpdateTournamentCommandMessage
            {
                Id = message.TournamentDto.Id,
                Name = message.TournamentDto.Name,
                GameId = message.TournamentDto.GameId
            });

            return new UpdateTournamentGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = true
                }
            };
        });
    }

    public ValueTask<DeleteTournamentByIdGrpcCommandResult> DeleteTournamentById(
        DeleteTournamentByIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            await _publishEndpoint.Publish(new DeleteTournamentCommandMessage
            {
                Id = message.Id
            });

            return new DeleteTournamentByIdGrpcCommandResult
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

    public ValueTask<GetMatchesByTournamentIdGrpcCommandResult> GetMatchesByTournamentId(
        GetMatchesByTurnamentIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var matches = await _entityDataService.ListEntities(
                new ExpressionFilterDefinition<MatchEntity>(entity => entity.TurnamentId == message.TurnamentId));

            return new GetMatchesByTournamentIdGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = true
                },
                MatchDtos = _mapper.Map<IEnumerable<MatchDto>>(matches)
            };
        });
    }

    public ValueTask<GetMatchesByTeamIdGrpcCommandResult> GetMatchesByTeamId(
        GetMatchesByTeamIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var matches = await _entityDataService.ListEntities(
                new ExpressionFilterDefinition<MatchEntity>(entity => entity.TeamsId.Contains(message.TeamId)));

            return new GetMatchesByTeamIdGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = true
                },
                MatchDtos = _mapper.Map<IEnumerable<MatchDto>>(matches)
            };
        });
    }

    public ValueTask<GetMatchByIdGrpcCommandResult> GetMatchById(GetMatchByIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var match = await _entityDataService.GetEntity<MatchEntity>(message.Id);

            return new GetMatchByIdGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata
                {
                    Success = true
                },
                MatchDto = _mapper.Map<MatchDto>(match)
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