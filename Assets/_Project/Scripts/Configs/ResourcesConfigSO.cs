using _Project.Scripts.Core.ResourceStorage;
using AYellowpaper.SerializedCollections;
using UnityEngine;

namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Configs/ResourcesConfigSO", fileName = "ResourcesConfigSO", order = 0)]
    public class ResourcesConfigSO : ScriptableObject
    {
        public SerializedDictionary<ResourceSO,int> resources;
    }
}