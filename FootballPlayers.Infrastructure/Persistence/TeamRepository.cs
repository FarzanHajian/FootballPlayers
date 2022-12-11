using Dapper;
using Dapper.Contrib.Extensions;
using FootballPlayers.Abstractions.Persistence;
using FootballPlayers.Entities;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;

namespace FootballPlayers.Infrastructure.Persistence
{
    internal class TeamRepository : RepositoryBase, ITeamRepository
    {
        public TeamRepository(IConfiguration config) : base(config)
        {
        }

        public IEnumerable<Team> FetchAll()
        {
            using SqliteConnection connection = new(connectionString);
            return connection.GetAll<Team>();
        }

        public bool Exists(int id)
        {
            using SqliteConnection connection = new(connectionString);
            return connection.ExecuteScalar<int>("select count(Id) from Team where Id=@Id", new { Id = id }) == 1;
        }
    }
}