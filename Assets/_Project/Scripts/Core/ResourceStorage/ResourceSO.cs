using UnityEngine;

namespace _Project.Scripts.Core.ResourceStorage
{
    [CreateAssetMenu(menuName = "Create ResourceSO", fileName = "ResourceSO", order = 0)]
    public class ResourceSO : ScriptableObject
    {
        public string resourceName;
        public Sprite sprite;
    }
}   