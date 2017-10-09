using Microsoft.Practices.Unity;
using Prism.Regions;
using Verbs.Infrastructure;
using Verbs.Infrastructure.Base;
using Verbs.Spanish.Views;

namespace Verbs.Spanish.Module
{
    public class SpanishModule : PrismBaseModule
    {
        public SpanishModule(IUnityContainer unityContainer, IRegionManager regionManager) : base(unityContainer, regionManager)
        {
            RegionManager.RegisterViewWithRegion(RegionNames.ContentRegion, typeof(SpanishPanel));
        }
    }
}
