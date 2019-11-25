using System;
using System.Data;
using System.Configuration;
using System.Data.SqlClient;
using System.Collections.Generic;

using NUnit.Framework;

using Starter.Bootstrapper;
using Starter.Data.Entities;
using Starter.Data.Repositories;

namespace Starter.Repository.Tests
{
    /// <summary>
    /// Base class for the Starter.Repository.Tests project
    /// </summary>
    [SetUpFixture]
    public class TestsBase
    {
        protected ICatRepository CatRepository;

        protected List<Cat> Cats;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            Setup.Bootstrap();

            CatRepository = IocWrapper.Instance.GetService<ICatRepository>();
        }

        /// <summary>
        /// Cleans up database created records
        /// </summary>
        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            var connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "truncate table Cats";

                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
            }
        }

        /// <summary>
        /// Creates tests data in the database
        /// </summary>
        protected void CreateTestData()
        {
            Cats = new List<Cat>
            {
                new Cat
                {
                    Id = Guid.NewGuid(), SecondaryId = Guid.NewGuid(), Name = "Widget", AbilityId = Ability.Eating
                },
                new Cat
                {
                    Id = Guid.NewGuid(), SecondaryId = Guid.NewGuid(), Name = "Garfield",
                    AbilityId = Ability.Engineering
                },
                new Cat
                {
                    Id = Guid.NewGuid(), SecondaryId = Guid.NewGuid(), Name = "Mr. Boots", AbilityId = Ability.Lounging
                }
            };

            var connectionString = ConfigurationManager.ConnectionStrings["DatabaseConnection"].ConnectionString;

            using (var connection = new SqlConnection(connectionString))
            using (var command = connection.CreateCommand())
            {
                command.CommandText = "CreateCat";
                command.CommandType = CommandType.StoredProcedure;

                foreach (var cat in Cats)
                {
                    SqlParameter[] parameters =
                    {
                        new SqlParameter("Id", cat.Id),
                        new SqlParameter("SecondaryId", cat.SecondaryId),
                        new SqlParameter("Name", cat.Name),
                        new SqlParameter("AbilityId", cat.AbilityId),
                    };

                    command.Parameters.Clear();
                    command.Parameters.AddRange(parameters);

                    connection.Open();
                    command.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}