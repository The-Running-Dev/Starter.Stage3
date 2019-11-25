using System.Threading;
using System.Threading.Tasks;

using Microsoft.Extensions.Hosting;

namespace Starter.MessageBus.Consumer
{
    public class MessageBusConsumer : BackgroundService
    {
        public MessageBusConsumer()
        {
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }

        protected override Task ExecuteAsync(CancellationToken stoppingToken)
        {
            stoppingToken.ThrowIfCancellationRequested();

            return Task.CompletedTask;
        }
    }
}