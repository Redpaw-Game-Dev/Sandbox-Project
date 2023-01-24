using Scripts.Abstraction;
using UnityEngine;
using UnityEngine.InputSystem.Layouts;
using Zenject;

namespace Scripts.Input
{
    public class InputJoystick : InputInteractiveObject
    {
        private enum JoystickType 
        {
            Fixed, Floating, Dynamic
        }
        
        [InputControl(layout = "Vector2"), SerializeField] protected string _controlPath;
        [SerializeField] private JoystickType _joystickType;
        [SerializeField] protected float _joystickRadius = 50;
        [SerializeField] private RectTransform _area;
        [SerializeField] protected RectTransform _background;
        [SerializeField] private RectTransform _joystick;
        
        [Inject(Id = "UI Camera")] protected Camera _uiCamera;
        private Vector3 _startPos;
        
        protected override string controlPathInternal
        {
            get => _controlPath;
            set => _controlPath = value;
        }
        
        private void Start()
        {
            _startPos = _background.anchoredPosition;
        }
        
        protected override void BeforeOnPointerDownTasks(PointerInfo pointerInfo)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_area, pointerInfo.Position, _uiCamera,
                out var localPoint);
            if (_joystickType != JoystickType.Fixed)
            {
                _background.anchoredPosition = localPoint;
                _joystick.anchoredPosition = Vector2.zero;
            }
        }

        protected override void BeforeOnPointerUpTasks(PointerInfo pointerInfo)
        {
            _background.anchoredPosition = _startPos;
            _joystick.anchoredPosition = Vector2.zero;
            SendValueToControl(Vector2.zero);
        }
        
        protected override void BeforeOnPointerDragTasks(PointerInfo pointerInfo)
        {
            RectTransformUtility.ScreenPointToLocalPointInRectangle(_area, pointerInfo.Position, _uiCamera,
                out var localPoint);
            var delta = localPoint - _background.anchoredPosition;
            if (_joystickType == JoystickType.Dynamic && delta.magnitude > _joystickRadius)
            {
                _background.anchoredPosition = localPoint - delta.normalized * _joystickRadius;
            }
            delta = Vector2.ClampMagnitude(delta, _joystickRadius);
            _joystick.anchoredPosition = delta;
            var input = new Vector2(delta.x / _joystickRadius, delta.y / _joystickRadius);
            SendValueToControl(input);
        }
    }
}