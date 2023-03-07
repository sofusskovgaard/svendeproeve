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

    public ValueTask<GetTournamentsGrpcCommandResult> GetTournaments(GetTournamentsGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var filters = new List<FilterDefinition<TournamentEntity>>();

            if (!string.IsNullOrEmpty(message.EventId))
            {
                filters.Add(new FilterDefinitionBuilder<TournamentEntity>().Eq(entity => entity.EventId, message.EventId));
            }

            if (!string.IsNullOrEmpty(message.GameId))
            {
                filters.Add(new FilterDefinitionBuilder<TournamentEntity>().Eq(entity => entity.GameId, message.GameId));
            }

            var entities = await _entityDataService.ListEntities<TournamentEntity>(filter =>
                filters.Any() ? filter.And(filters) : FilterDefinition<TournamentEntity>.Empty);

            return new GetTournamentsGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata{ Success = true },
                Data = _mapper.Map<TournamentDto[]>(entities)
            };
        });
    }

    public ValueTask<GetTournamentByMatchIdGrpcCommandResult> GetTournamentByMatchId(GetTournamentByMatchIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var entity = await _entityDataService.GetEntity<TournamentEntity>(filter => filter.AnyEq(entity => entity.MatchesId, message.MatchId));

            return new GetTournamentByMatchIdGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata{ Success = true },
                Data = _mapper.Map<TournamentDto>(entity)
            };
        });
    }

    public ValueTask<GetTournamentByIdGrpcCommandResult> GetTournamentById(GetTournamentByIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var entity = await _entityDataService.GetEntity<TournamentEntity>(message.Id);

            return new GetTournamentByIdGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata{ Success = true },
                Data = _mapper.Map<TournamentDto>(entity)
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

            return new CreateTournamentGrpcCommandResult{ Metadata = new GrpcCommandResultMetadata { Success = true } };
        });
    }

    public ValueTask<UpdateTournamentGrpcCommandResult> UpdateTournament(UpdateTournamentGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            await _publishEndpoint.Publish(new UpdateTournamentCommandMessage
            {
                Id = message.Id,
                Name = message.Name,
                GameId = message.GameId
            });

            return new UpdateTournamentGrpcCommandResult{ Metadata = new GrpcCommandResultMetadata { Success = true } };
        });
    }

    public ValueTask<DeleteTournamentByIdGrpcCommandResult> DeleteTournamentById(DeleteTournamentByIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            await _publishEndpoint.Publish(new DeleteTournamentCommandMessage{ Id = message.Id });
            return new DeleteTournamentByIdGrpcCommandResult{ Metadata = new GrpcCommandResultMetadata { Success = true } };
        });
    }

    #endregion

    #region Matches

    public ValueTask<GetMatchesGrpcCommandResult> GetMatches(GetMatchesGrpcCommandMessage message)
    {
        return this.TryAsync(async () =>
        {
            var filters = new List<FilterDefinition<MatchEntity>>();

            if (!string.IsNullOrEmpty(message.TeamId))
            {
                filters.Add(new FilterDefinitionBuilder<MatchEntity>().AnyEq(entity => entity.TeamsId, message.TeamId));
            }

            if (!string.IsNullOrEmpty(message.TournamentId))
            {
                filters.Add(new FilterDefinitionBuilder<MatchEntity>().Eq(entity => entity.TournamentId, message.TournamentId));
            }

            if (!string.IsNullOrEmpty(message.WinningTeamId))
            {
                filters.Add(new FilterDefinitionBuilder<MatchEntity>().Eq(entity => entity.WinningTeamId, message.WinningTeamId));
            }

            var entities = await _entityDataService.ListEntities<MatchEntity>(filter =>
                filters.Any() ? filter.And(filters) : FilterDefinition<MatchEntity>.Empty);

            return new GetMatchesGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata { Success = true },
                Data = _mapper.Map<MatchDto[]>(entities)
            };
        });
    }

    public ValueTask<GetMatchByIdGrpcCommandResult> GetMatchById(GetMatchByIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            var entity = await _entityDataService.GetEntity<MatchEntity>(message.Id);

            return new GetMatchByIdGrpcCommandResult
            {
                Metadata = new GrpcCommandResultMetadata{ Success = true },
                Data = _mapper.Map<MatchDto>(entity)
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
                TournamentId = message.TournamentId
            });

            return new CreateMatchGrpcCommandResult{ Metadata = new GrpcCommandResultMetadata{ Success = true } };
        });
    }

    public ValueTask<UpdateMatchGrpcCommandResult> UpdateMatch(UpdateMatchGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            await _publishEndpoint.Publish(new UpdateMatchCommandMessage
            {
                Id = message.Id,
                Name = message.Name,
                WinningTeamId = message.WinningTeamId
            });

            return new UpdateMatchGrpcCommandResult{ Metadata = new GrpcCommandResultMetadata { Success = true } };
        });
    }

    public ValueTask<DeleteMatchByIdGrpcCommandResult> DeleteMatchById(DeleteMatchByIdGrpcCommandMessage message)
    {
        return TryAsync(async () =>
        {
            await _publishEndpoint.Publish(new DeleteMatchCommandMessage{ Id = message.Id });
            return new DeleteMatchByIdGrpcCommandResult{ Metadata = new GrpcCommandResultMetadata { Success = true } };
        });
    }

    #endregion
}