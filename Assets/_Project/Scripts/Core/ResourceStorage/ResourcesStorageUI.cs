using System.Collections.Generic;
using _Project.Scripts.Configs;
using _Project.Scripts.Infrastructure;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.ResourceStorage
{
    public class ResourcesStorageUI : MonoBehaviour
    {
        private ResourcesConfigSO _resourcesConfig;
        private IResourceStorage _resourceStorage;
        
        private Dictionary<ResourceSO,SingleResourceUI> _resourcesUI;
        [SerializeField] private SingleResourceUI resourceUIPrefab;
        

        [Inject]
        private void Construct(IResourceStorage resourceStorage,
            StaticDataService staticDataService)
        {
            _resourceStorage = resourceStorage;
            _resourcesConfig = staticDataService.GetResourcesConfig();
        }
        
        private void Start()
        {
            _resourcesUI = new Dictionary<ResourceSO, SingleResourceUI>();
            _resourceStorage.OnResourceAmountChanged += UpdateResourceAmount;
            foreach (var resource in _resourcesConfig.resources)
            {
                var resourceUI = Instantiate(resourceUIPrefab, transform);
                resourceUI.SetResource(resource.Key); // TODO: use pool
                resourceUI.SetAmount(_resourceStorage.GetResourceAmount(resource.Key));
                _resourcesUI.Add(resource.Key, resourceUI);
            }
        }

        private void UpdateResourceAmount(ResourceSO changedResource)
        {
            _resourcesUI[changedResource].SetAmount(_resourceStorage.GetResourceAmount(changedResource));
        }
    }
}