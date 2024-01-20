using System;
using System.Collections.Generic;

namespace _Project.Scripts
{
    public interface IResourceStorage
    {
        void AddResource(ResourceSO resourceType, int amount);
        void RemoveResource(ResourceSO resourceType, int amount);
        bool HasEnoughResources(ResourceSO resourceType, int amount);
        int GetResourceAmount(ResourceSO resourceType);
        List<ResourceSO> GetResources();
        
        event Action<ResourceSO> OnResourceAmountChanged;
    }
}