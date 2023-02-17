using UnityEngine;

namespace Sandbox.Input
{
    public class DynamicJoystick : FloatJoystick
    {
        public override void PointerDrag(Vector2 pointerPosition)
        {
            
            UpdateLocalPoint(pointerPosition);
            Vector2 delta = GetDelta();
            if (delta.magnitude > _joystickRadius)
            {
                _background.anchoredPosition = _localPoint - delta.normalized * _joystickRadius;
            }
            delta = Vector2.ClampMagnitude(delta, _joystickRadius);
            _joystick.anchoredPosition = delta;
            _input = new Vector2(delta.x / _joystickRadius, delta.y / _joystickRadius);
        }
    }
}