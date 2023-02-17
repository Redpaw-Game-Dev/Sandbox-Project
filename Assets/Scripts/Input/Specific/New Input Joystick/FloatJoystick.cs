using UnityEngine;

namespace Sandbox.Input
{
    public class FloatJoystick : FixedJoystick
    {
        protected Vector2 _startPos;
        
        public override void PointerDown(Vector2 pointerPosition)
        {
            base.PointerDown(pointerPosition);
            _startPos = _background.anchoredPosition;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_area, pointerPosition, _uiCamera,
                out _localPoint);
            _background.anchoredPosition = _localPoint;
            _joystick.anchoredPosition = Vector2.zero;
        }

        public override void PointerUp()
        {
            _background.anchoredPosition = _startPos;
            base.PointerUp();
        }
    }
}