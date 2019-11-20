using System;
using System.Web.Http;
using System.Threading.Tasks;
using System.Collections.Generic;

using Starter.Data.Entities;
using Starter.Data.Repositories;

namespace Starter.API.Controllers
{
    public class CatController : ApiController
    {
        public CatController(ICatRepository repository)
        {
            _repository = repository;
        }

        // GET api/cat
        public async Task<IEnumerable<Cat>> GetAll()
        {
            return await _repository.GetAll();
        }

        // GET api/cat/5
        public async Task<Cat> GetById(Guid id)
        {
            return await _repository.GetById(id);
        }

        // POST api/cat
        public async Task Post([FromBody] Cat entity)
        {
            await _repository.Create(entity);
        }

        // PUT api/cat/{id}
        public async Task Put([FromBody] Cat entity)
        {
            await _repository.Update(entity);
        }

        // DELETE api/cat/{id}
        public async Task Delete(Guid id)
        {
            await _repository.Delete(id);
        }

        private readonly ICatRepository _repository;
    }
}