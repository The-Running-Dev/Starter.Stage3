using System.Windows;
using Starter.Bootstrapper;

using Unity;

namespace Starter.WPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App
    {
        protected override void OnStartup(StartupEventArgs e)
        {
            Setup.Bootstrap();

            var mainWindow = IocWrapper.Instance.Container.Resolve<MainWindow>();

            mainWindow.Show();
        }
    }
}