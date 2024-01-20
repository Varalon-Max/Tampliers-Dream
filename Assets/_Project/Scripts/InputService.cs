using System;
using UnityEngine;

namespace _Project.Scripts
{
    public class InputService : MonoBehaviour, IInputService
    {
        public Vector2 GetMousePosition()
        {
            return Input.mousePosition;
        }

        public Vector2 GetMovementInput()
        {
            return new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        }

        public event Action<float> OnRotationApplied;
        public event Action<float> OnZoomApplied;

        public event Action OnDragPanMovementActivation;
        public event Action OnDragPanMovementDeactivation;

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                OnDragPanMovementActivation?.Invoke();
            }

            if (Input.GetMouseButtonUp(0))
            {
                OnDragPanMovementDeactivation?.Invoke();
            }

            if (Input.mouseScrollDelta.y != 0)
            {
                OnZoomApplied?.Invoke(Input.mouseScrollDelta.y);
            }

            if (Input.GetMouseButton(1))
            {
                OnRotationApplied?.Invoke(Input.GetAxis("Mouse X"));
            }
        }
    }
}