using UnityEngine;

namespace _Project.Scripts
{
    [CreateAssetMenu(menuName = "Create ResourceSO", fileName = "ResourceSO", order = 0)]
    public class ResourceSO : ScriptableObject
    {
        public string resourceName;
        public Sprite sprite;
    }
}   