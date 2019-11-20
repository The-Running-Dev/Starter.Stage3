using System.Data;

namespace Starter.Data.Connections
{
    public interface IConnection
    {
        IDbConnection Connect();

        IDbCommand CreateSpCommand(string sql, IDbDataParameter[] paramArray);
    }
}