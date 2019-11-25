using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Starter.Data.Entities;
using Starter.Framework.Clients;
using Starter.Framework.Services;

namespace Starter.Data.Services
{
    public class CatService: ICatService
    {
        private readonly IApiClient _apiClient; 
        
        private readonly IMessageBus _messageBus;

        public CatService(IMessageBus messageBus, IApiClient apiClient)
        {
            _messageBus = messageBus;
            _apiClient = apiClient;
        }

        public async Task<IEnumerable<Cat>> GetAll()
        {
            return await _apiClient.GetAll<Cat>();
        }

        public async Task<Cat> GetById(Guid id)
        {
            return await _apiClient.GetById<Cat>(id);
        }

        public async Task Create(Cat entity)
        {
            var message = new Message<Cat>(MessageCommand.Create, entity);

            await _messageBus.Send(message);
        }

        public async Task Update(Cat entity)
        {
            var message = new Message<Cat>(MessageCommand.Update, entity);

            await _messageBus.Send(message);
        }

        public async Task Delete(Guid id)
        {
            var message = new Message<Guid>(MessageCommand.Delete, id);

            await _messageBus.Send(message);
        }
    }
}