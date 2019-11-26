using System;

using Starter.Data.Services;
using Starter.Data.Entities;
using Starter.Framework.Clients;

namespace Starter.MessageBus.Consumer
{
    public class MessageBusConsumer : IMessageBusConsumer
    {
        private readonly IMessageBus _messageBus;
        
        private readonly IApiClient _apiClient;

        public MessageBusConsumer(IMessageBus messageBus, IApiClient apiClient)
        {
            _messageBus = messageBus;
            _apiClient = apiClient;

            _messageBus.DataReceived += OnDataReceived;
        }

        /// <summary>
        /// Handles the data received from the message bus
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnDataReceived(object sender, Message<Cat> e)
        {
            switch (e.Command)
            {
                case MessageCommand.Create:
                    _apiClient.Create(e.Entity);

                    break;
                case MessageCommand.Update:
                    _apiClient.Update(e.Entity);

                    break;
                case MessageCommand.Delete:
                    _apiClient.Delete(e.Entity.Id);

                    break;
            }

            Console.WriteLine($"{e.Command}, {e.Type}, {e.Entity.Id}");
        }   

        public bool Start()
        {
            _messageBus.Receive<Cat>();

            return true;
        }

        public bool Stop()
        {
            _messageBus.Stop();
            
            return true;
        }
    }
}