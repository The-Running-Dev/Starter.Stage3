using System;
using System.Threading;
using System.Threading.Tasks;
using System.Timers;
using Starter.Data.Connections;
using Topshelf;

using Starter.Data.Services;


namespace Starter.ServiceBus.Consumer
{
    

    public class ServiceBusConsumer
    {
        public ServiceBusConsumer(ICatService catService)
        {
        }

        public void Start()
        {
        }

        public void Stop()
        {
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            var rc = HostFactory.Run(x =>
            {
                //x.Service<TownCrier>(s =>
                //{
                //    s.ConstructUsing(name => new ServiceBusConsumer());
                //    s.WhenStarted(tc => tc.Start());
                //    s.WhenStopped(tc => tc.Stop());
                //});
                //x.RunAsLocalSystem();

                //x.SetDescription("Sample Topshelf Host");
                //x.SetDisplayName("Stuff");
                //x.SetServiceName("Stuff");
            });

            var exitCode = (int)Convert.ChangeType(rc, rc.GetTypeCode());
            Environment.ExitCode = exitCode;
        }
    }
}
