using UnityEngine;
using UnityEngine.UI;

namespace _Project.Scripts.Core.ResourceStorage
{
    public class SingleResourceUI : MonoBehaviour
    {
        [SerializeField] private TMPro.TextMeshProUGUI amountText;
        [SerializeField] private Image resourceSprite;
        
        private ResourceSO _resource;
        public void SetResource(ResourceSO resource)
        {
            _resource = resource;
            resourceSprite.sprite = resource.sprite;
        }

        public void SetAmount(int getResourceAmount)
        {
            amountText.text = getResourceAmount.ToString();
        }
    }
}