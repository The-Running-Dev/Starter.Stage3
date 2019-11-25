using System.Threading.Tasks;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using Starter.Framework.Services;
using Starter.Framework.Extensions;

namespace Starter.MessageBus.RabbitMQ
{
    public class RabbitMessageBus : IMessageBus
    {
        private readonly IModel _channel;

        public RabbitMessageBus()
        {
            var factory = new ConnectionFactory { HostName = "localhost" };

            // create connection
            var connection = factory.CreateConnection();

            // create channel
            _channel = connection.CreateModel();

            _channel.ExchangeDeclare("demo.exchange", ExchangeType.Topic);
            _channel.QueueDeclare("demo.queue.log", false, false, false, null);
            _channel.QueueBind("demo.queue.log", "demo.exchange", "demo.queue.*", null);
            _channel.BasicQos(0, 1, false);

            //_connection.ConnectionShutdown += RabbitMQ_ConnectionShutdown;
        }

        public async Task Send<T>(T entity)
        {
            await Task.Run(() =>
            {
                var entityAsBytes = entity.ToJsonBytes();

                _channel.BasicPublish("", "hello", null, entityAsBytes);
            });
        }

        public async Task<T> Receive<T>() where T : new()
        {
            var entity = await Task.Run(() =>
            {
                var result = new T();
                var consumer = new EventingBasicConsumer(_channel);

                consumer.Received += (model, ea) =>
                {
                    result = ea.Body.FromJsonBytes<T>();
                };

                _channel.BasicConsume("hello", true, consumer);

                return result;
            });

            return entity;
        }
    }
}