using System;
using System.Text;
using System.Threading.Tasks;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;

using Starter.Framework.Clients;
using Starter.Framework.Extensions;

namespace Starter.Framework.Services
{
    public class ServiceBus : IServiceBus
    {
        public async Task Send<T>(T entity)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                await Task.Run(() =>
                {
                    var entityAsBytes = Encoding.UTF8.GetBytes(entity.ToJson());

                    channel.QueueDeclare("hello", false, false, false, null);
                    channel.BasicPublish("", "hello", null, entityAsBytes);
                });
            }
        }

        public async Task<T> Receive<T>() where T : new()
        {
            T entity;
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            using (var channel = connection.CreateModel())
            {
                entity = await Task.Run(() =>
                {
                    var result = new T();
                    var consumer = new EventingBasicConsumer(channel);

                    channel.QueueDeclare("hello", false, false, false, null);
                    consumer.Received += (model, ea) =>
                    {
                        result = Encoding.UTF8.GetString(ea.Body).FromJson<T>();
                    };

                    channel.BasicConsume("hello", true, consumer);

                    return result;
                });
            }

            return entity;
        }

        private readonly IApiClient _apiClient;
    }
}