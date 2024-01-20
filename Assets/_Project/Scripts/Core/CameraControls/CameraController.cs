using _Project.Scripts.Configs;
using _Project.Scripts.Infrastructure;
using Cinemachine;
using UnityEngine;
using Zenject;

namespace _Project.Scripts.Core.CameraControls
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private Transform target;
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

        [SerializeField] private bool useEdgeScrolling;
        [SerializeField] private InputService inputService;

        private bool _dragPanMoveActive;
        private Vector2 _lastMousePosition;
        private CameraControllerConfigSO _config;

        [Inject]
        private void Construct(StaticDataService staticDataService)
        {
            _config = staticDataService.GetCameraControllerConfig();
        }

        private void Update()
        {
            HandleMovement();
        }

        private void Start()
        {
            inputService.OnRotationApplied += RotateTarget;
            inputService.OnZoomApplied += Zoom;

            inputService.OnDragPanMovementActivation += ActivateDragPanMovement;
            inputService.OnDragPanMovementDeactivation += DeactivateDragPanMovement;
        }

        private void DeactivateDragPanMovement()
        {
            _dragPanMoveActive = false;
        }

        private void ActivateDragPanMovement()
        {
            _dragPanMoveActive = true;

            _lastMousePosition = Input.mousePosition;
        }

        private void Zoom(float zoom)
        {
            float targetFov = cinemachineVirtualCamera.m_Lens.FieldOfView;

            targetFov += zoom * _config.zoomDistance;
            targetFov = Mathf.Clamp(targetFov, _config.minFov, _config.maxFov);

            cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(cinemachineVirtualCamera.m_Lens.FieldOfView,
                targetFov,
                Time.deltaTime * _config.zoomSpeed);
        }

        private void HandleMovement()
        {
            var movementInput = inputService.GetMovementInput();
            var horizontal = movementInput.x;
            var vertical = movementInput.y;

            Vector2 mousePosition = inputService.GetMousePosition();

            if (useEdgeScrolling)
            {
                ReadEdgeScrollingInput(mousePosition, ref horizontal, ref vertical);
            }

            if (_dragPanMoveActive)
            {
                var delta = mousePosition - _lastMousePosition;

                _lastMousePosition = mousePosition;
                horizontal = -delta.x * _config.dragPanSpeedModifier;
                vertical = -delta.y * _config.dragPanSpeedModifier;
            }

            MoveTarget(horizontal, vertical);
        }

        private void ReadEdgeScrollingInput(Vector2 mousePosition, ref float horizontal, ref float vertical)
        {
            if (mousePosition.x < _config.edgeScrollingBorderThickness)
            {
                horizontal = -1f;
            }
            else if (mousePosition.x > Screen.width - _config.edgeScrollingBorderThickness)
            {
                horizontal = 1f;
            }

            if (mousePosition.y < _config.edgeScrollingBorderThickness)
            {
                vertical = -1f;
            }
            else if (mousePosition.y > Screen.height - _config.edgeScrollingBorderThickness)
            {
                vertical = 1f;
            }
        }

        private void MoveTarget(float horizontal, float vertical)
        {
            target.position += target.forward * (vertical * _config.speed * Time.deltaTime);
            target.position += target.right * (horizontal * _config.speed * Time.deltaTime);
        }

        private void RotateTarget(float rotation)
        {
            target.transform.RotateAround(target.position, Vector3.up,
                rotation * _config.rotationSpeed * Time.deltaTime);
        }
    }
}