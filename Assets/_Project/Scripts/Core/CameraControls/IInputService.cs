using System;
using UnityEngine;

namespace _Project.Scripts.Core.CameraControls
{
    public interface IInputService
    {
        Vector2 GetMousePosition();
        Vector2 GetMovementInput();

        event Action<float> OnRotationApplied;
        event Action<float> OnZoomApplied;

        event Action OnDragPanMovementActivation;
        event Action OnDragPanMovementDeactivation;
    }
}