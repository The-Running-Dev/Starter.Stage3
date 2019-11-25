using System;

using Topshelf;

using Starter.Data.Services;

namespace Starter.MessageBus.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            var catService = Bootstrapper.IocWrapper.Instance.GetService<ICatService>();

            var rc = HostFactory.Run(x =>
            {
                x.Service<MessageBusConsumer>(s =>
                {
                    s.ConstructUsing(name => new MessageBusConsumer(catService));
                    s.WhenStarted(tc => tc.Start());
                    s.WhenStopped(tc => tc.Stop());
                });
                x.RunAsLocalSystem();

                x.SetDescription("RabbitMQ Consumer Host");
                //x.SetDisplayName("Stuff");
                //x.SetServiceName("Stuff");
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
