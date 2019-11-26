using Starter.Data.Entities;

namespace Starter.Data.Services
{
    /// <summary>
    /// Defines the contract for the message bus consumer
    /// </summary>
    public interface IMessageBusConsumer
    {
        void OnDataReceived(object sender, Message<Cat> e);

        bool Start();

        bool Stop();
    }
}