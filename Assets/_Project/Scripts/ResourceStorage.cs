using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

namespace _Project.Scripts
{
    public class ResourceStorage : IResourceStorage, IInitializable
    {
        private ResourcesConfigSO _resourcesConfig;
        private Dictionary<ResourceSO, int> _resources;
        public event Action<ResourceSO> OnResourceAmountChanged;

        public void Initialize()
        {
            _resources = new Dictionary<ResourceSO, int>();
            foreach (var resource in _resourcesConfig.resources)
            {
                _resources.Add(resource.Item1, resource.Item2);
            }
        }

        public void AddResource(ResourceSO resourceType, int amount)
        {
            _resources[resourceType] += amount;
            
            OnResourceAmountChanged?.Invoke(resourceType);
        }

        public void RemoveResource(ResourceSO resourceType, int amount)
        {
            if (_resources[resourceType] < amount)
            {
                Debug.LogError("Not enough resources");
            }

            _resources[resourceType] -= amount;
            
            OnResourceAmountChanged?.Invoke(resourceType);
        }

        public bool HasEnoughResources(ResourceSO resourceType, int amount)
        {
            return _resources[resourceType] >= amount;
        }

        public int GetResourceAmount(ResourceSO resourceType)
        {
            return _resources[resourceType];
        }

        public List<ResourceSO> GetResources()
        {
            return new List<ResourceSO>(_resources.Keys);
        }
    }
}