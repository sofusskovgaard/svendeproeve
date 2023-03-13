using System.ComponentModel;
using System.Security.Cryptography;
using App.Data.Extensions;
using App.Data.Services;
using App.Infrastructure.Extensions;
using App.Infrastructure.Options;
using App.Services.Authentication.Data.Entities;
using App.Services.Authentication.Infrastructure.Services;
using App.Services.Departments.Data.Entities;
using App.Services.Events.Data.Entities;
using App.Services.Games.Data.Entities;
using App.Services.Orders.Common.Constants;
using App.Services.Orders.Data.Entities;
using App.Services.Organizations.Data.Entities;
using App.Services.Teams.Data.Entities;
using App.Services.Tournaments.Data.Entities;
using App.Services.Users.Data.Entities;
using Bogus;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson;

namespace Seeding
{
    public class Seeder
    {
        private readonly IServiceProvider _services;

        public Seeder()
        {
            var services = new ServiceCollection();

            Environment.SetEnvironmentVariable("MONGO_URI", "mongodb://mongo:mongo@localhost:27017/");

            services.AddOptions<DatabaseOptions>();
            services.AddMongoDb();

            _services = services.BuildServiceProvider();
        }

        [Fact]
        public async Task Seed()
        {
            var entityDataService = _services.GetRequiredService<IEntityDataService>();

            #region Departments

            var department1 = new DepartmentEntity()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "Sønderborg",
                Address = "Hilmar Finsens Gade 18, 6400 Sønderborg, Denmark"
            };

            var department2 = new DepartmentEntity()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "Aabenraa",
                Address = "Stegholt 35, 6200 Aabenraa, Denmark"
            };

            var department3 = new DepartmentEntity()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "Flensburg",
                Address = "Ritterstraße 27, 24939 Flensburg, Germany"
            };

            var department4 = new DepartmentEntity()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "Schleswig",
                Address = "Fjordallee 1, 24837 Schleswig, Germany"
            };

            #endregion

            #region Games

            var game1 = new GameEntity()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "Counter-Strike: Global Offensive",
                Description =
                    "Counter-Strike: Global Offensive (CS: GO) expands upon the team-based action gameplay that it pioneered when it was launched 19 years ago. CS: GO features new maps, characters, weapons, and game modes, and delivers updated versions of the classic CS content (de_dust2, etc.).",
                Genre = new []
                {
                    "Action",
                    "Shooter",
                    "FPS",
                    "Tactical"
                },
                CoverPicture = "https://cdn.cloudflare.steamstatic.com/steam/apps/730/header.jpg?t=1668125812"
            };

            var game2 = new GameEntity()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "Dota 2",
                Description =
                    "Every day, millions of players worldwide enter battle as one of over a hundred Dota heroes. And no matter if it's their 10th hour of play or 1,000th, there's always something new to discover. With regular updates that ensure a constant evolution of gameplay, features, and heroes, Dota 2 has taken on a life of its own.",
                Genre = new[]
                {
                    "MOBA",
                    "Strategy"
                },
                CoverPicture = "https://cdn.cloudflare.steamstatic.com/steam/apps/570/header.jpg?t=1678300512"
            };

            var game3 = new GameEntity()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "iRacing",
                Description =
                    "We are the world’s premier motorsports racing game. An iRacing membership provides entry into the newest form of motorsport: internet racing. Internet racing is a fun, easy, and inexpensive way for race fan and gamers alike to enjoy the thrill of the racetrack from the comfort of their home.",
                Genre = new[]
                {
                    "Racing",
                    "Simulation",
                    "Driving"
                },
                CoverPicture = "https://cdn.cloudflare.steamstatic.com/steam/apps/266410/header.jpg?t=1678213579"
            };

            

            #endregion

            #region Organization

            var organizationFaker = new Faker<OrganizationEntity>()
                .RuleFor(entity => entity.Id, () => ObjectId.GenerateNewId().ToString())
                .RuleFor(entity => entity.Name, faker => faker.Company.CompanyName())
                .RuleFor(entity => entity.Bio, faker => faker.Company.Bs());


            var organizations = organizationFaker.Generate(10);

            #endregion

            #region Users

            var _password = "Test123!";
            var hasherResponse = Hasher.Hash(_password);

            var userFaker = new Faker<UserEntity>()
                .RuleFor(entity => entity.Id, () => ObjectId.GenerateNewId().ToString())
                .RuleFor(entity => entity.Firstname, faker => faker.Person.FirstName)
                .RuleFor(entity => entity.Lastname, faker => faker.Person.LastName)
                .RuleFor(entity => entity.Username, faker => faker.Person.Email)
                .RuleFor(entity => entity.Email, faker => faker.Person.Email)
                .RuleFor(entity => entity.DateOfBirth, faker => faker.Person.DateOfBirth)
                .RuleFor(entity => entity.Organizations, faker => new []{ faker.PickRandom(organizations.Select(x => x.Id)) })
                .FinishWith((_, entity) =>
                {
                    var org = organizations.First(x => x.Id == entity.Organizations.First());
                    org.MemberIds = org.MemberIds?.Concat(new[] { entity.Id! }).ToArray() ?? new []{ entity.Id! };
                });

            var users = userFaker.Generate(100).DistinctBy(entity => entity.Username).DistinctBy(entity => entity.Email);

            var userLogins = users.Select(entity =>
            {
                return new UserLoginEntity()
                {
                    Id = entity.Id,
                    Username = entity.Username,
                    Email = entity.Email,
                    PasswordHash = hasherResponse.Hash,
                    PasswordSalt = hasherResponse.Salt
                };
            });

            hasherResponse = Hasher.Hash(_password);

            var user1 = new UserEntity()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Firstname = "Sofus",
                Lastname = "Skovgaard",
                Username = "sofus",
                Email = "sofus.skovgaard@gmail.com"
            };

            var userLogin1 = new UserLoginEntity()
            {
                Id = user1.Id,
                Email = user1.Email,
                Username = user1.Username,
                IsAdmin = true,
                PasswordHash = hasherResponse.Hash,
                PasswordSalt = hasherResponse.Salt
            };

            hasherResponse = Hasher.Hash(_password);

            var user2 = new UserEntity()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Firstname = "Joachim",
                Lastname = "Tøfting",
                Username = "joachim",
                Email = "joachim.toefting@gmail.com"
            };
            
            var userLogin2 = new UserLoginEntity()
            {
                Id = user2.Id,
                Email = user2.Email,
                Username = user2.Username,
                IsAdmin = true,
                PasswordHash = hasherResponse.Hash,
                PasswordSalt = hasherResponse.Salt
            };

            hasherResponse = Hasher.Hash(_password);

            var user3 = new UserEntity()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Firstname = "Allan",
                Lastname = "Holm Iversen",
                Username = "allan",
                Email = "allan.holm.iversen@gmail.com"
            };
            
            var userLogin3 = new UserLoginEntity()
            {
                Id = user3.Id,
                Email = user3.Email,
                Username = user3.Username,
                IsAdmin = true,
                PasswordHash = hasherResponse.Hash,
                PasswordSalt = hasherResponse.Salt
            };

            

            #endregion

            #region Teams

            var teamFaker = new Faker<TeamEntity>()
                .RuleFor(entity => entity.Id, () => ObjectId.GenerateNewId().ToString())
                .RuleFor(entity => entity.Name, faker => faker.Company.CompanyName())
                .RuleFor(entity => entity.GameId, faker => faker.PickRandomParam(game1.Id, game2.Id, game3.Id))
                .RuleFor(entity => entity.MembersId, faker => new []
                {
                    faker.PickRandom(users).Id,
                    faker.PickRandom(users).Id,
                    faker.PickRandom(users).Id,
                    faker.PickRandom(users).Id,
                    faker.PickRandom(users).Id,
                })
                .RuleFor(entity => entity.ManagerId, faker => faker.PickRandom(users).Id)
                .RuleFor(entity => entity.OrganizationId, faker => faker.PickRandom(organizations).Id)
                .FinishWith((faker, entity) =>
                {
                    var _users = users.Where(x => entity.MembersId.Any(y => y == x.Id));

                    foreach (var user in _users)
                    {
                        user.Teams = user.Teams?.Concat(new[] { entity.Id }).ToArray() ?? new[] { entity.Id };
                    }

                    var _manager = users.FirstOrDefault(x =>
                        x.Id == entity.ManagerId && (x.Teams?.All(y => y != entity.Id) ?? false));
                    if (_manager != null) _manager.Teams = _manager.Teams?.Concat(new[] { entity.Id }).ToArray() ?? new[] { entity.Id };

                    var _organization = organizations.First(x => x.Id == entity.OrganizationId);

                    _organization.TeamIds = _organization.TeamIds?.Concat(new[] { entity.Id! }).ToArray() ??
                                            new[] { entity.Id! };
                });

            var teams = teamFaker.Generate(organizations.Count * 5).DistinctBy(entity => entity.Name);

            #endregion

            #region Events

            var event1 = new EventEntity()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                DepartmentId = department1.Id,
                EventName = "EUC Syd's Årlige League",
                StartDate = new DateTime(2023, 3, 31, 12, 0, 0),
                EndDate = new DateTime(2023, 3, 31, 22, 0, 0),
                Location = department1.Address
            };

            var tournamentFaker = new Faker<TournamentEntity>()
                .RuleFor(entity => entity.Id, () => ObjectId.GenerateNewId().ToString())
                .RuleFor(entity => entity.Name, faker => $"Tournament {faker.IndexFaker}")
                .RuleFor(entity => entity.EventId, () => event1.Id)
                .RuleFor(entity => entity.GameId, faker => faker.PickRandomParam(game1.Id, game2.Id, game3.Id))
                .FinishWith((_, entity) =>
                {
                    event1.Tournaments =
                        event1.Tournaments?.Concat(new[] { entity.Id! }).ToArray() ?? new[] { entity.Id! };
                });

            var tournaments = tournamentFaker.Generate(3);

            var matchFaker = new Faker<MatchEntity>()
                .RuleFor(entity => entity.Id, () => ObjectId.GenerateNewId().ToString())
                .RuleFor(entity => entity.Name, faker => $"Match {faker.IndexFaker}")
                .RuleFor(entity => entity.TournamentId, faker => faker.PickRandom(tournaments).Id)
                .RuleFor(entity => entity.TeamsId, (faker, entity) =>
                {
                    var _tournament = tournaments.First(x => x.Id == entity.TournamentId);
                    var _teams = teams.Where(x => x.GameId == _tournament.GameId);

                    return new[]
                    {
                        faker.PickRandom(_teams).Id,
                        faker.PickRandom(_teams).Id
                    };
                })
                .FinishWith((faker, entity) =>
                {
                    var _tournament = tournaments.First(x => x.Id == entity.TournamentId);
                    _tournament.MatchesId = _tournament.MatchesId?.Concat(new[] { entity.Id! }).ToArray() ?? new[] { entity.Id! };
                });

            var matches = matchFaker.Generate(tournaments.Count * 5);

            #endregion

            #region Products

            var product1 = new ProductEntity()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "Early Bird Ticket",
                Description = "Get your ticket early and receive a early bird discount!",
                ReferenceType = ProductReferenceType.Event,
                ReferenceId = event1.Id,
                Price = 199.99M
            };

            var product2 = new ProductEntity()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "Ticket",
                Description = "Get your ticket now!",
                ReferenceType = ProductReferenceType.Event,
                ReferenceId = event1.Id,
                Price = 299.99M
            };

            var product3 = new ProductEntity()
            {
                Id = ObjectId.GenerateNewId().ToString(),
                Name = "Late Bird Ticket",
                Description = "You were a little slow so the tickets are extra expensive. Get yours quick or they might be gone.",
                ReferenceType = ProductReferenceType.Event,
                ReferenceId = event1.Id,
                Price = 349.99M
            };

            #endregion

            await entityDataService.SaveEntities(department1, department2, department3, department4);
            await entityDataService.SaveEntities(game1, game2, game3);
            await entityDataService.SaveEntities(product1, product2, product3);
            await entityDataService.SaveEntity(event1);
            await entityDataService.SaveEntities(tournaments);
            await entityDataService.SaveEntities(matches);
            await entityDataService.SaveEntities(teams);
            await entityDataService.SaveEntities(users);
            await entityDataService.SaveEntities(userLogins);
            await entityDataService.SaveEntities(user1, user2, user3);
            await entityDataService.SaveEntities(userLogin1, userLogin2, userLogin3);
            await entityDataService.SaveEntities(organizations);
        }
    }
}