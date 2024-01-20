using _Project.Scripts.Configs;
using _Project.Scripts.Infrastructure.AssetProviders;
using Zenject;

namespace _Project.Scripts.Infrastructure
{
    public class StaticDataService : IInitializable
    {
        private IAssetProvider _assetProvider;
        private CameraControllerConfigSO _cameraControllerConfigSO;
        private ResourcesConfigSO _resourcesConfigSO;
        public StaticDataService(IAssetProvider assetProvider)
        {
            _assetProvider = assetProvider;
        }

        // Get all configs

        public CameraControllerConfigSO GetCameraControllerConfig()
        {
            return _cameraControllerConfigSO;
        }

        public ResourcesConfigSO GetResourcesConfig()
        {
            return _resourcesConfigSO;
        }

        public void Initialize()
        {
            LoadCameraControllerConfig();
            LoadResourcesConfig();
        }

        private void LoadResourcesConfig()
        {
           _resourcesConfigSO= _assetProvider.Load<ResourcesConfigSO>(AssetPath.RESOURCES_CONFIG);
        }

        private void LoadCameraControllerConfig()
        {
            _cameraControllerConfigSO = _assetProvider.Load<CameraControllerConfigSO>(AssetPath.CAMERA_CONTROLLER_CONFIG);
        }
    }
}