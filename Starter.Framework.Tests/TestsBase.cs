using System.Data;
using Microsoft.SqlServer.Server;

using NUnit.Framework;

using Starter.Bootstrapper;

namespace Starter.Framework.Tests
{
    /// <summary>
    /// 
    /// </summary>
    [SetUpFixture]
    public class TestsBase
    {
        protected SqlDataRecord SqlDataRecord;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Setup.Bootstrap();

            SqlDataRecord = new SqlDataRecord(
                new SqlMetaData("Id", SqlDbType.UniqueIdentifier),
                new SqlMetaData("Name", SqlDbType.NVarChar, 20));
        }
    }
}