using System;
using System.Threading.Tasks;

using Starter.Data.Entities;

namespace Starter.Data.Services
{
    /// <summary>
    /// Defines the contract for the message bus
    /// </summary>
    public interface IMessageBus
    {
        event EventHandler<Message<Cat>> DataReceived;

        Task Send<T>(T entity);

        void Receive<T>() where T : new();

        void Stop();
    }
}