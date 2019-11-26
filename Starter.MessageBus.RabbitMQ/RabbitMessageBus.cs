using System;
using System.Threading.Tasks;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using Starter.Data.Entities;
using Starter.Data.Services;
using Starter.Framework.Extensions;

namespace Starter.MessageBus.RabbitMQ
{
    /// <summary>
    /// 
    /// </summary>
    public class RabbitMessageBus : IMessageBus
    {
        private readonly IModel _channel;

        private readonly EventingBasicConsumer _consumer;
        
        private readonly IConnection _connection;
        
        private readonly string _queueName;

        public event EventHandler<Message<Cat>> DataReceived;

        public RabbitMessageBus()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            _connection = factory.CreateConnection();
            _channel = _connection.CreateModel();
            _consumer = new EventingBasicConsumer(_channel);
            
            _queueName = "Starter.Queue";
            _channel.QueueDeclare(_queueName, false, false, false, null);
        }
        
        public async Task Send<T>(T entity)
        {
            await Task.Run(() =>
            {
                var entityAsBytes = entity.ToJsonBytes();

                _channel.BasicPublish(string.Empty, _queueName, null, entityAsBytes);
            });
        }

        public void Receive<T>() where T : new()
        {
            _consumer.Received += (model, args) =>
            {
                var message = args.Body.FromJsonBytes<Message<Cat>>();

                DataReceived?.Invoke(this, message);
            };

            _channel.BasicConsume(_queueName, true, _consumer);
        }

        public void Stop()
        {
            _channel.Close();
            _connection.Close();
        }
    }
}