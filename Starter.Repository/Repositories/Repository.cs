using System.Data;
using System.Threading.Tasks;
using System.Collections.Generic;
using Dapper;
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

        public async Task<IEnumerable<T>> ExecuteQuery<T>(string sql, object parameters = null)
        {
            using (var connection = _connection.Create())
            {
                return await connection.QueryAsync<T>(sql, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        public async Task ExecuteNonQuery(string sql, object parameters = null)
        {
            using (var connection = _connection.Create())
            {
                await connection.ExecuteAsync(sql, parameters, commandType: CommandType.StoredProcedure);
            }
        }

        private readonly IConnection _connection;
    }
}