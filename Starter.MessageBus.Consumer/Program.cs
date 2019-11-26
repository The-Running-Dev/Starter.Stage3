using System;

using Topshelf;

using Starter.Bootstrapper;
using Starter.Data.Services;
using Starter.Data.Repositories;
using Starter.Framework.Clients;

namespace Starter.MessageBus.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Setup.Bootstrap();

            //HostFactory.New(x =>
            //{
            //    x.Service<MessageBusConsumer>(() => IocWrapper.Instance.GetService<MessageBusConsumer>());
            //});

            var messageBus = IocWrapper.Instance.GetService<IMessageBus>();
            var apiClient = IocWrapper.Instance.GetService<IApiClient>();

            var rc = HostFactory.Run(x =>
            {
                x.Service<MessageBusConsumer>(s =>
                {
                    s.ConstructUsing(name => new MessageBusConsumer(messageBus, apiClient));
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("MessageBus Consumer");
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
