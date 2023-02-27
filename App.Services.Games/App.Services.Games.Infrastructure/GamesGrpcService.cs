using App.Data.Services;
using App.Infrastructure.Grpc;
using App.Services.Games.Infrastructure.Grpc;
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
    }
}