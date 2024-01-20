using UnityEngine;
using UnityEngine.Serialization;

namespace _Project.Scripts.Configs
{
    [CreateAssetMenu(menuName = "Configs/CameraControllerConfig", fileName = "CameraControllerConfig", order = 0)]
    public class CameraControllerConfigSO : ScriptableObject
    {
        [Header("Camera Movement")]
        public float speed = 10f;
        public float edgeScrollingBorderThickness = 20f;
        public float dragPanSpeedModifier = 0.05f;
        [Header("Camera Rotation")]
        public float rotationSpeed = 100f;
        [Header("Camera Zoom")]
        public float zoomDistance = 5f;
        public float zoomSpeed = 50f;
        public float minFov = 15f;
        public float maxFov = 90f;
    }
}