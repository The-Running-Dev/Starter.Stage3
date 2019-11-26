using System;
using System.Threading.Tasks;
using System.Collections.Generic;

using Starter.Data.Entities;
using Starter.Framework.Clients;

namespace Starter.Data.Services
{
    public class CatService : ICatService, IDisposable
    {
        private readonly IApiClient _apiClient;

        private IMessageBus _messageBus;

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
            var message = new Message<Cat>(MessageCommand.Delete, new Cat { Id = id });

            await _messageBus.Send(message);
        }

        public void Dispose()
        {
            _messageBus.Stop();
            _messageBus = null;
        }
    }
}