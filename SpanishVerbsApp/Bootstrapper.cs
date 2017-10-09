using Microsoft.Practices.Unity;
using Prism.Unity;
using Verbs.DemoApp.Views;
using System.Windows;
using Prism.Modularity;

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

        protected override void ConfigureModuleCatalog()
        {
            ModuleCatalog moduleCatalog = (ModuleCatalog) this.ModuleCatalog;
            moduleCatalog.AddModule(typeof(Verbs.Spanish.Module.SpanishModule));
        }
    }
}
