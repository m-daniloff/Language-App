using Microsoft.Practices.Unity;
using Prism.Unity;
using Verbs.DemoApp.Views;
using System.Windows;

namespace Verbs.DemoApp
{
    internal class Bootstrapper : UnityBootstrapper
    {
        protected override DependencyObject CreateShell()
        {
            return Container.Resolve<MainWindow>();
        }

        protected override void InitializeShell()
        {
            Application.Current.MainWindow.Show();
        }
    }
}
