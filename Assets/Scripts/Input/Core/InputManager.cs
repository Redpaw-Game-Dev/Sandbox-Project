using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Scripts.Abstraction;
using Scripts.ObjectsManagement;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using EnhancedTouch = UnityEngine.InputSystem.EnhancedTouch;
using Zenject;
using Object = UnityEngine.Object;

namespace Scripts.Input
{
    public class InputManager : IInitializable, ITickable
    {
        private readonly int _mousePointerIndex = 0;
        
        private PointerInfo[] _pointers = new PointerInfo[10];
        private IInteractive[] _activeInteractives = new IInteractive[10];
        private IInteractive[] _selectedInteractives = new IInteractive[10];
        private InputActionsConfig _inputActionsConfig;
        private bool _isMoving;
        private bool _isUpdating;
        private Camera _mainCamera;

        public InputActionsConfig InputActionsConfig => _inputActionsConfig;
        
        public event Action<PointerInfo, IInteractive> OnPointerDown;
        public event Action<PointerInfo, IInteractive> OnPointerUp;
        public event Action<PointerInfo, IInteractive> OnPointerMove;
        public event Action<PointerInfo, IInteractive> OnPointerEnter;
        public event Action<PointerInfo, IInteractive> OnPointerExit;
        public event Action<PointerInfo, IInteractive> OnPointerDrag;
        public event Action<PointerInfo, IInteractive> OnPointerHold;
        public event Action<Vector2Info> OnMove;

        [Inject]
        private void Construct(ObjectsManager objectsManager)
        {
            _mainCamera = objectsManager.GetObject<Camera>("MainCamera", OnFoundCameraCallback);
        }

        private void OnFoundCameraCallback(Object obj)
        {
            _mainCamera = (Camera)obj;
        }

        public void Initialize()
        {
            for (var i = 0; i < _pointers.Length; i++)
            {
                _pointers[i].Id = i;
            }
            _inputActionsConfig = new InputActionsConfig();
            _inputActionsConfig.Base.PointerMove.performed += HandlePointerMoveAction;
            _inputActionsConfig.Base.PointerClick.started += HandlePointerDownAction;
            _inputActionsConfig.Base.PointerClick.canceled += HandlePointerUpAction;
            _inputActionsConfig.Base.Move.started += HandleMoveStarted;
            _inputActionsConfig.Base.Move.canceled += HandleMoveCanceled;
            _inputActionsConfig.Base.Enable();
            EnhancedTouch.EnhancedTouchSupport.Enable();
            EnhancedTouch.Touch.onFingerDown += HandleFingerDown;
            EnhancedTouch.Touch.onFingerUp += HandleFingerUp;
            EnhancedTouch.Touch.onFingerMove += HandleFingerMove;
            UpdatePointerWorldPosition();
        }
        
        public void Tick()
        {
            for (int i = 0; i < _pointers.Length; i++)
            {
                if (_selectedInteractives[i] != null && !_selectedInteractives[i].IsHeld)
                {
                    var tempInteractive = _selectedInteractives[i];
                    if (tempInteractive.PressTime >= tempInteractive.HoldThresholdTime)
                    {
                        tempInteractive.PointerHold(_pointers[i]);
                        OnPointerHold?.Invoke(_pointers[i], _selectedInteractives[i]);
                    }
                }
            }
            if (_isMoving)
            {
                var moveInput = _inputActionsConfig.Base.Move.ReadValue<Vector2>();
                if (moveInput != Vector2.zero)
                { 
                    OnMove?.Invoke(new Vector2Info(moveInput));
                }
               
            }
        }

        //TODO: Fix pointers world position in build
        private async void UpdatePointerWorldPosition()
        {
            if (!_isUpdating)
            {
                _isUpdating = true;
                await UniTask.WaitUntil(() => _mainCamera != null);
                while (_isUpdating)
                {
                    for (var i = 0; i < _pointers.Length; i++)
                    {
                        Vector2 pointerPoint =  _pointers[i].Position;
                        float depth = _mainCamera.nearClipPlane;
                        if (_selectedInteractives[i] != null)
                        {
                            depth = _mainCamera.WorldToScreenPoint(_selectedInteractives[i].Transform.position).z;
                        }
                        else if (_activeInteractives[i] != null)
                        {
                            depth = _mainCamera.WorldToScreenPoint(_activeInteractives[i].Transform.position).z;
                        }
                        _pointers[i].WorldPosition  = _mainCamera.ScreenToWorldPoint(new Vector3(pointerPoint.x, pointerPoint.y, depth));
                    }
                    await UniTask.Yield();
                }
            }
        }
        
        private void HandleFingerDown(EnhancedTouch.Finger finger)
        {
            TouchPointerDown(finger.index, finger.currentTouch.screenPosition);
        }
        
        private void HandleFingerUp(EnhancedTouch.Finger finger)
        {
            PointerUp(finger.index, true);
        }
        
        private void HandleFingerMove(EnhancedTouch.Finger finger)
        {
            PointerMove(finger.index, finger.currentTouch.screenPosition);
        }

        private void HandlePointerDownAction(InputAction.CallbackContext context)
        {
            PointerDown(_mousePointerIndex);
        }
        
        private void HandlePointerUpAction(InputAction.CallbackContext context)
        {
            PointerUp(_mousePointerIndex, false);
        }

        private void HandlePointerMoveAction(InputAction.CallbackContext context)
        {
            PointerMove(_mousePointerIndex,  context.ReadValue<Vector2>());
        }

        private void HandleMoveStarted(InputAction.CallbackContext context)
        {
            _isMoving = true;
        }
        
        private void HandleMoveCanceled(InputAction.CallbackContext context)
        {
            _isMoving = false;
        }
        
        private void UpdateActiveInteractive(int index)
        {
            var position = _pointers[index].Position;
            PointerEventData eventData = new PointerEventData(EventSystem.current)
            {
                position = position
            };
            var eventRaycastResult = new List<RaycastResult>();
            EventSystem.current?.RaycastAll(eventData, eventRaycastResult);
            IInteractive interactive = null;
            eventRaycastResult.FirstOrDefault(result => result.gameObject.TryGetComponent(out interactive));
            
            if (_mainCamera && Physics.Raycast(_mainCamera.ScreenPointToRay(position), out var raycastHit))
            {
                if (interactive == null) interactive = raycastHit.transform.gameObject.GetComponent<IInteractive>();
            }

            if (_activeInteractives[index] != interactive)
            {
                if (_activeInteractives[index] != null)
                {
                    _activeInteractives[index]?.PointerExit(_pointers[index]);
                    OnPointerExit?.Invoke(_pointers[index], _activeInteractives[index]);
                }
                
                if (interactive == null || interactive.IsInteractive)
                {
                    _activeInteractives[index] = interactive;
                }
                else
                {
                    _activeInteractives[index] = null;
                }

                if (_activeInteractives[index] != null)
                {
                    _activeInteractives[index].PointerEnter(_pointers[index]);
                    OnPointerEnter?.Invoke(_pointers[index], _activeInteractives[index]);
                }
            }
        }

        private void PointerMove(int index, Vector2 pointerPosition)
        {
            _pointers[index].Delta = pointerPosition - _pointers[index].Position;
            _pointers[index].Position = pointerPosition;
            UpdateActiveInteractive(index);
            if (_activeInteractives[index] != null)
            {
                _activeInteractives[index].PointerMove(_pointers[index]);
                OnPointerMove?.Invoke(_pointers[index], _activeInteractives[index]);
            }
            
            if (_selectedInteractives[index] != null)
            {
                _selectedInteractives[index]?.PointerDrag(_pointers[index]);
                OnPointerDrag?.Invoke(_pointers[index], _selectedInteractives[index]);
            }
        }
        
        private void PointerDown(int index)
        {
            _pointers[index].Contact = true;
            if (_activeInteractives[index] is { IsInteractive: true })
            {
                _selectedInteractives[index] = _activeInteractives[index];
            }
            
            if (_selectedInteractives[index] != null)
            {
                _selectedInteractives[index].PointerDown(_pointers[index]);
                OnPointerDown?.Invoke(_pointers[index], _selectedInteractives[index]);
            }
        }
        
        private void TouchPointerDown(int index, Vector2 position)
        {
            _pointers[index].Position = position;
            UpdateActiveInteractive(index);
            PointerDown(index);
        }
        
        private void PointerUp(int index, bool touchPointer)
        {
            _pointers[index].Contact = false;
            if (_selectedInteractives[index] != null)
            {
                _selectedInteractives[index].PointerUp(_pointers[index]);
                OnPointerUp?.Invoke(_pointers[index], _selectedInteractives[index]);
                _selectedInteractives[index] = null; 
            }
            
            if (touchPointer)
            {
                if (_selectedInteractives[index] != null)
                {
                    _activeInteractives[index]?.PointerExit(_pointers[index]);
                    OnPointerExit?.Invoke(_pointers[index], _activeInteractives[index]);
                    _activeInteractives[index] = null;
                } 
                _pointers[index].Position = Vector2.zero;
                _pointers[index].Delta = Vector2.zero;
            }
        }
    }
}