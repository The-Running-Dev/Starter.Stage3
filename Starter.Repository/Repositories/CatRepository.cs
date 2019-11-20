using System;
using System.Data;
using System.Linq;
using System.Data.SqlClient;
using System.Threading.Tasks;
using System.Collections.Generic;

using Starter.Data.Entities;
using Starter.Data.Connections;
using Starter.Data.Repositories;

namespace Starter.Repository.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    public class CatRepository : Repository, ICatRepository
    {
        public CatRepository(IConnection connection) : base(connection)
        {
        }

        public async Task<IEnumerable<Cat>> GetAll()
        {
            return await ExecuteQueryAsync<Cat>(GetAllSp);
        }

        public async Task<Cat> GetById(Guid id)
        {
            var entities = await ExecuteQueryAsync<Cat>(GetByIdSp, new IDbDataParameter[] { new SqlParameter("id", id) });

            return entities.FirstOrDefault();
        }

        public async Task Create(Cat entity)
        {
            await ExecuteNonQueryAsync(CreateSp, new IDbDataParameter[]
            {
                new SqlParameter("id", entity.Id),
                new SqlParameter("name", entity.Name),
                new SqlParameter("abilityId", entity.AbilityId)
            });
        }

        public async Task Update(Cat entity)
        {
            await ExecuteNonQueryAsync(UpdateSp, new IDbDataParameter[]
            {
                new SqlParameter("id", entity.Id),
                new SqlParameter("name", entity.Name),
                new SqlParameter("abilityId", entity.AbilityId)
            });
        }

        public async Task Delete(Guid id)
        {
            await ExecuteNonQueryAsync(DeleteSp, new IDbDataParameter[]
            {
                new SqlParameter("id", id)
            });
        }

        private readonly string GetAllSp = "GetAllCats";

        private readonly string GetByIdSp = "GetCatById";

        private readonly string CreateSp = "CreateCat";

        private readonly string UpdateSp = "UpdateCat";

        private readonly string DeleteSp = "DeleteCat";
    }
}