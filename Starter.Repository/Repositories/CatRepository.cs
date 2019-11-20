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
            return await ExecuteQuery<Cat>(GetAllSp);
        }

        public async Task<Cat> GetById(Guid id)
        {
            var entities = await ExecuteQuery<Cat>(GetByIdSp, new { id });

            return entities.FirstOrDefault();
        }

        public async Task Create(Cat entity)
        {
            await ExecuteNonQuery(CreateSp, new { entity.Id, entity.Name, entity.AbilityId });
        }

        public async Task Update(Cat entity)
        {
            await ExecuteNonQuery(UpdateSp, new { entity.Id, entity.Name, entity.AbilityId });
        }

        public async Task Delete(Guid id)
        {
            await ExecuteNonQuery(DeleteSp, new { id });
        }

        private readonly string GetAllSp = "GetAllCats";

        private readonly string GetByIdSp = "GetCatById";

        private readonly string CreateSp = "CreateCat";

        private readonly string UpdateSp = "UpdateCat";

        private readonly string DeleteSp = "DeleteCat";
    }
}