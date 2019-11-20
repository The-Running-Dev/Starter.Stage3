using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;

using Starter.Data.Connections;
using Starter.Framework.Extensions;

namespace Starter.Repository.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class Repository
    {
        public Repository(IConnection connection)
        {
            _connection = connection;
        }

        public async Task<IEnumerable<T>> ExecuteQueryAsync<T>(string sql, IDbDataParameter[] parameters = null)
        {
            using (var command = _connection.CreateSpCommand(sql, parameters))
            {
                return await ExecuteReader<T>(command);
            }
        }

        public async Task ExecuteNonQueryAsync(string sql, IDbDataParameter[] parameters = null)
        {
            using (var command = _connection.CreateSpCommand(sql, parameters))
            {
                await Task.Run(command.ExecuteNonQuery);
            }
        }

        public async Task<IEnumerable<T>> ExecuteReader<T>(IDbCommand command)
        {
            var entities = new List<T>();

            await Task.Run(() =>
            {
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        entities.Add(reader.Map<T>());
                    }
                }
            });

            return entities;
        }

        private readonly IConnection _connection;
    }
}