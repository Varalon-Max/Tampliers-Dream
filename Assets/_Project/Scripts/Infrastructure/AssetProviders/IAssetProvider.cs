using UnityEngine;

namespace _Project.Scripts.Infrastructure
{
    public interface IAssetProvider
    {
        TAsset Load<TAsset>(string key) where TAsset : Object;
        TAsset[] LoadAll<TAsset>(string key) where TAsset : Object;
    }
}