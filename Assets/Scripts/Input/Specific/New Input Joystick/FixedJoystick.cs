using UnityEngine;
using Zenject;

namespace Sandbox.Input
{
    public class FixedJoystick : IJoystick
    {
        [SerializeField] protected float _joystickRadius = 50;
        [SerializeField] protected RectTransform _area;
        [SerializeField] protected RectTransform _background;
        [SerializeField] protected RectTransform _joystick;
        
        [Inject(Id = "UI Camera")] protected Camera _uiCamera;
        protected Vector2 _input;
        protected Vector2 _localPoint;

        public Vector2 Input => _input;

        public virtual void PointerDown(Vector2 pointerPosition)
        {
            
        }

        public virtual void PointerDrag(Vector2 pointerPosition)
        {
            UpdateLocalPoint(pointerPosition);
            Vector2 delta = GetDelta();
            delta = Vector2.ClampMagnitude(delta, _joystickRadius);
            _joystick.anchoredPosition = delta;
            _input = new Vector2(delta.x / _joystickRadius, delta.y / _joystickRadius);
        }

        public virtual void PointerUp()
        {
            _joystick.anchoredPosition = Vector2.zero;
            _input = Vector2.zero;
        }

        protected void UpdateLocalPoint(Vector2 screenPoint) =>
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_area, screenPoint, _uiCamera, out _localPoint);
        
        protected Vector2 GetDelta() => _localPoint - _background.anchoredPosition;
    }
}