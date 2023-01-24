using Scripts.Input;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Zenject;

namespace Scripts.Abstraction
{
    public class DragInteractiveInWorldSpaceBehaviour : SerializedMonoBehaviour
    {
        [OdinSerialize] private IInteractive _interactive;
        
        [Inject(Id = "Main Camera")] private Camera _mainCamera;
        
        private Vector3 _grabOffset;
        private float _depth;

        private void Update()
        {
            if (_interactive.IsPressed)
            {
                //TODO: Use Pointer.WorldPosition instead Pointer.Position after the fix pointers world position in build
                var pointerPosition = _interactive.Pointer.Position;
                var worldPoint = GetPointerWorldPosition(new Vector3(pointerPosition.x, pointerPosition.y, _depth));
                transform.position = worldPoint + _grabOffset;
            }
        }

        private void SetOffset(PointerInfo pointerInfo)
        {
            var pointerPoint = pointerInfo.Position;
            var objectPosition = transform.position;
            _depth = _mainCamera.WorldToScreenPoint(objectPosition).z;
            var worldPoint = GetPointerWorldPosition(new Vector3(pointerPoint.x, pointerPoint.y, _depth));
            _grabOffset = objectPosition - worldPoint;
        }

        private Vector3 GetPointerWorldPosition(Vector3 screenPosition) => _mainCamera.ScreenToWorldPoint(screenPosition);

        private void OnEnable()
        {
            _interactive.OnPointerDown += SetOffset;
        }

        private void OnDisable()
        {
            _interactive.OnPointerDown -= SetOffset;
        }
    }
}