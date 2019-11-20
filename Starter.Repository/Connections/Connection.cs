using System;
using System.Data;

using System.Data.SqlClient;

using Starter.Data.Connections;
using Starter.Framework.Extensions;

namespace Starter.Repository.Connections
{
    public class Connection : IConnection
    {
        public Connection(string connectionString)
        {
            _connectionString = connectionString ?? throw new ArgumentNullException(nameof(connectionString));
        }

        public IDbConnection Create()
        {
            var connection = new SqlConnection(_connectionString);

            connection.Open();

            return connection;
        }

        public IDbCommand CreateSpCommand(string sql, IDbDataParameter[] parameters)
        {
            var connection = Create();
            var command = connection.CreateCommand();

            command.CommandType = CommandType.StoredProcedure;
            command.CommandText = sql;
            command.AddParameters(parameters);

            return command;
        }

        private readonly string _connectionString;
    }
}