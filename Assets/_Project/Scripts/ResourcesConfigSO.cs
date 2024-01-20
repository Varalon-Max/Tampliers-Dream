using System.Collections.Generic;
using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(menuName = "Configs/ResourcesConfigSO", fileName = "ResourcesConfigSO", order = 0)]
    public class ResourcesConfigSO : ScriptableObject
    {
        public List<(ResourceSO,int)> resources;
    }
}