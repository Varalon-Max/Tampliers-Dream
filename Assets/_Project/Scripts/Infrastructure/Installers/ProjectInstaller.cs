using _Project.Scripts.Core.ResourceStorage;
using Zenject;

namespace _Project.Scripts.Infrastructure.Installers
{
    public class ProjectInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAssetProvider();

            BindStaticDataService();
            
            // Container.BindInterfacesAndSelfTo<ResourceStorage>().AsSingle();

        }

        private void BindStaticDataService()
        {
            Container.BindInterfacesAndSelfTo<StaticDataService>().AsSingle().NonLazy();
        }

        private void BindAssetProvider()
        {
            Container.Bind<IAssetProvider>().To<AssetProvider>().AsSingle();
        }
    }
}