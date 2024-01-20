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

        private bool _dragPanMoveActive;
        private Vector2 _lastMousePosition;

        private void Update()
        {
            HandleMovement();
            HandleRotation();
            HandleZoom();
        }

        private void HandleZoom()
        {
            float targetFov = cinemachineVirtualCamera.m_Lens.FieldOfView;

            float zoom = 0f;
            if (Input.mouseScrollDelta.y > 0)
            {
                zoom = -1f;
            }
            else if (Input.mouseScrollDelta.y < 0)
            {
                zoom = 1f;
            }

            targetFov += zoom * config.zoomDistance;
            targetFov = Mathf.Clamp(targetFov, config.minFov, config.maxFov);

            cinemachineVirtualCamera.m_Lens.FieldOfView = Mathf.Lerp(cinemachineVirtualCamera.m_Lens.FieldOfView,
                targetFov,
                Time.deltaTime * config.zoomSpeed);
        }

        private void HandleRotation()
        {
            float rotation = 0f;
            if (Input.GetKey(KeyCode.Q))
            {
                rotation = 1f;
            }
            else if (Input.GetKey(KeyCode.E))
            {
                rotation = -1f;
            }

            RotateTarget(rotation);
        }

        private void HandleMovement()
        {
            var horizontal = Input.GetAxis("Horizontal");
            var vertical = Input.GetAxis("Vertical");

            if (useEdgeScrolling)
            {
                ReadEdgeScrollingInput(ref horizontal, ref vertical);
            }

            if (Input.GetMouseButtonDown(1))
            {
                _dragPanMoveActive = true;

                _lastMousePosition = Input.mousePosition;
            }

            if (Input.GetMouseButtonUp(1))
            {
                _dragPanMoveActive = false;
            }

            if (_dragPanMoveActive)
            {
                var delta = (Vector2)Input.mousePosition - _lastMousePosition;

                _lastMousePosition = Input.mousePosition;
                horizontal = -delta.x * config.dragPanSpeedModifier;
                vertical = -delta.y * config.dragPanSpeedModifier;
            }

            MoveTarget(horizontal, vertical);
        }

        private void ReadEdgeScrollingInput(ref float horizontal, ref float vertical)
        {
            if (Input.mousePosition.x < config.edgeScrollingBorderThickness)
            {
                horizontal = -1f;
            }
            else if (Input.mousePosition.x > Screen.width - config.edgeScrollingBorderThickness)
            {
                horizontal = 1f;
            }

            if (Input.mousePosition.y < config.edgeScrollingBorderThickness)
            {
                vertical = -1f;
            }
            else if (Input.mousePosition.y > Screen.height - config.edgeScrollingBorderThickness)
            {
                vertical = 1f;
            }
        }

        private void MoveTarget(float horizontal, float vertical)
        {
            target.transform.position += target.transform.forward * (vertical * config.speed * Time.deltaTime);
            target.transform.position += target.transform.right * (horizontal * config.speed * Time.deltaTime);
        }

        private void RotateTarget(float rotation)
        {
            target.transform.RotateAround(target.position, Vector3.up,
                rotation * config.rotationSpeed * Time.deltaTime);
        }
    }
}