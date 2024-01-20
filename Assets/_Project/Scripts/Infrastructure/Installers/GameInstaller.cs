using _Project.Scripts.Core.ResourceStorage;
using Zenject;

namespace _Project.Scripts.Infrastructure.Installers
{
    public class GameInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindResourceStorage();
        }

        private void BindResourceStorage()
        {
            Container.BindInterfacesAndSelfTo<ResourceStorage>().AsSingle();
        }
    }
}