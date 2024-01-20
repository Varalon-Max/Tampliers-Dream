using _Project.Scripts.Configs;
using Cinemachine;
using UnityEngine;

namespace _Project.Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private CameraControllerConfigSO config;
        [SerializeField] private Transform target;
        [SerializeField] private CinemachineVirtualCamera cinemachineVirtualCamera;

        [SerializeField] private bool useEdgeScrolling;
        [SerializeField] private InputService inputService;

        private bool _dragPanMoveActive;
        private Vector2 _lastMousePosition;

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

            targetFov += zoom * config.zoomDistance;
            targetFov = Mathf.Clamp(targetFov, config.minFov, config.maxFov);

            cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(cinemachineVirtualCamera.m_Lens.FieldOfView,
                targetFov,
                Time.deltaTime * config.zoomSpeed);
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
                horizontal = -delta.x * config.dragPanSpeedModifier;
                vertical = -delta.y * config.dragPanSpeedModifier;
            }

            MoveTarget(horizontal, vertical);
        }

        private void ReadEdgeScrollingInput(Vector2 mousePosition, ref float horizontal, ref float vertical)
        {
            if (mousePosition.x < config.edgeScrollingBorderThickness)
            {
                horizontal = -1f;
            }
            else if (mousePosition.x > Screen.width - config.edgeScrollingBorderThickness)
            {
                horizontal = 1f;
            }

            if (mousePosition.y < config.edgeScrollingBorderThickness)
            {
                vertical = -1f;
            }
            else if (mousePosition.y > Screen.height - config.edgeScrollingBorderThickness)
            {
                vertical = 1f;
            }
        }

        private void MoveTarget(float horizontal, float vertical)
        {
            target.position += target.forward * (vertical * config.speed * Time.deltaTime);
            target.position += target.right * (horizontal * config.speed * Time.deltaTime);
        }

        private void RotateTarget(float rotation)
        {
            target.transform.RotateAround(target.position, Vector3.up,
                rotation * config.rotationSpeed * Time.deltaTime);
        }
    }
}