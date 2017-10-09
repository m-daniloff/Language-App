using Microsoft.Practices.Unity;
using Prism.Modularity;
using Prism.Regions;

namespace Verbs.Infrastructure.Base
{
    public abstract class PrismBaseModule : IModule
    {
        public PrismBaseModule(IUnityContainer unityContainer, IRegionManager regionManager)
        {
            UnityContainer = unityContainer;
            RegionManager = regionManager;
        }
        public void Initialize()
        {
        }

        public IUnityContainer UnityContainer { get; private set; }
        public IRegionManager RegionManager { get; private set; }
    }
}
