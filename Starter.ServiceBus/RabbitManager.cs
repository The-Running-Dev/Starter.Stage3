﻿using System;
using System.Text;

using RabbitMQ.Client;
using Microsoft.Extensions.ObjectPool;

namespace Starter.ServiceBus
{
    public class RabbitManager : IRabbitManager
    {
        private readonly DefaultObjectPool<IModel> _objectPool;

        public RabbitManager(IPooledObjectPolicy<IModel> objectPolicy)
        {
            _objectPool = new DefaultObjectPool<IModel>(objectPolicy, Environment.ProcessorCount * 2);
        }

        public void Publish<T>(T message, string exchangeName, string exchangeType, string routeKey)
            where T : class
        {
            if (message == null)
                return;

            var channel = _objectPool.Get();

            try
            {
                channel.ExchangeDeclare(exchangeName, exchangeType, true, false, null);

                var sendBytes = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));

                var properties = channel.CreateBasicProperties();
                properties.Persistent = true;

                channel.BasicPublish(exchangeName, routeKey, properties, sendBytes);
            }
            finally
            {
                _objectPool.Return(channel);
            }
        }
    }
}