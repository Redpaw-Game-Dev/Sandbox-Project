using System;
using Scripts.Abstraction;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Scripts.Input
{
    public abstract class InteractiveObject : SerializedMonoBehaviour, IInteractive
    {
        [SerializeField] protected bool _isInteractive = true;
        [SerializeField] protected float _holdThresholdTime = 0.5f;
        [SerializeField, FoldoutGroup("Tasks")] protected Task[] _onPointerDownTasks = new Task[0];
        [SerializeField, FoldoutGroup("Tasks")] protected Task[] _onPointerUpTasks = new Task[0];
        [SerializeField, FoldoutGroup("Tasks")] protected Task[] _onPointerMoveTasks = new Task[0];
        [SerializeField, FoldoutGroup("Tasks")] protected Task[] _onPointerEnterTasks = new Task[0];
        [SerializeField, FoldoutGroup("Tasks")] protected Task[] _onPointerExitTasks = new Task[0];
        [SerializeField, FoldoutGroup("Tasks")] protected Task[] _onPointerDragTasks = new Task[0];
        [SerializeField, FoldoutGroup("Tasks")] protected Task[] _onPointerHoldTasks = new Task[0];
        [SerializeField, FoldoutGroup("Tasks")] protected Task[] _onEnabledTasks = new Task[0];
        [SerializeField, FoldoutGroup("Tasks")] protected Task[] _onDisabledTasks = new Task[0];
        [SerializeField, FoldoutGroup("Tasks")] protected Task[] _onUpdateTasks = new Task[0];

        protected bool _isPressed;
        protected float _pressTime;
        protected bool _isHeld;
        protected bool _isPointerEntered;
        protected bool _isDragged;
        protected PointerInfo _pointer;

        public bool IsInteractive => _isInteractive;
        public float HoldThresholdTime => _holdThresholdTime;
        public bool IsPressed => _isPressed;
        public float PressTime => _pressTime;
        public bool IsHeld => _isHeld;
        public bool IsPointerEntered => _isPointerEntered;
        public bool IsDragged => _isDragged;
        public PointerInfo Pointer => _pointer;
        public Transform Transform => transform;

        public event Action<PointerInfo> OnPointerDown;
        public event Action<PointerInfo> OnPointerUp;
        public event Action<PointerInfo> OnPointerMove;
        public event Action<PointerInfo> OnPointerEnter;
        public event Action<PointerInfo> OnPointerExit;
        public event Action<PointerInfo> OnPointerDrag;
        public event Action<PointerInfo> OnPointerHold;
        public event Action OnEnabled;
        public event Action OnDisabled;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            InjectTasks(diContainer, _onPointerDownTasks);
            InjectTasks(diContainer, _onPointerUpTasks);
            InjectTasks(diContainer, _onPointerMoveTasks);
            InjectTasks(diContainer, _onPointerEnterTasks);
            InjectTasks(diContainer, _onPointerExitTasks);
            InjectTasks(diContainer, _onPointerDragTasks);
            InjectTasks(diContainer, _onPointerHoldTasks);
            InjectTasks(diContainer, _onEnabledTasks);
            InjectTasks(diContainer, _onDisabledTasks);
            InjectTasks(diContainer, _onUpdateTasks);
        }
        
        private void Update()
        {
            if (_isPressed)
            {
                _pressTime += Time.deltaTime;
            }
            BeforeOnUpdateTasks();
            DoTasks(_onUpdateTasks, _pointer);
            AfterOnUpdateTasks();
        }
        
        protected virtual void OnEnable()
        {
            DoTasks(_onEnabledTasks);
            AfterOnEnabledTasks();
            OnEnabled?.Invoke();
        }

        protected virtual void OnDisable()
        {
            BeforeOnDisabledTasks();
            DoTasks(_onDisabledTasks);
            OnDisabled?.Invoke();
        }
        
        public virtual void SetActive(bool value)
        {
            gameObject.SetActive(value);
        }

        public virtual void SetIsInteractive(bool value)
        {
            _isInteractive = value;
        }
        
        public void PointerDown(PointerInfo pointerInfo)
        {
            if (_isInteractive && !_isPressed)
            { 
                _isPressed = true;
                _pressTime = 0f;
                _pointer = pointerInfo;
                BeforeOnPointerDownTasks(pointerInfo);
                DoTasks(_onPointerDownTasks, _pointer);
                AfterOnPointerDownTasks(pointerInfo);
                OnPointerDown?.Invoke(pointerInfo);
            }
        }

        public void PointerUp(PointerInfo pointerInfo)
        {
            if (_isInteractive && _isPressed)
            { 
                _isPressed = false;
                _pressTime = 0f;
                _isHeld = false;
                _isDragged = false;
                _pointer = pointerInfo;
                BeforeOnPointerUpTasks(pointerInfo);
                DoTasks(_onPointerUpTasks, _pointer);
                AfterOnPointerUpTasks(pointerInfo);
                OnPointerUp?.Invoke(pointerInfo);
            }
        }

        public void PointerMove(PointerInfo pointerInfo)
        {
            if (_isInteractive && _isPointerEntered)
            {
                _pointer = pointerInfo;
                BeforeOnPointerMoveTasks(pointerInfo);
                DoTasks(_onPointerMoveTasks, _pointer);
                AfterOnPointerMoveTasks(pointerInfo);
                OnPointerMove?.Invoke(pointerInfo);
            }
        }

        public void PointerDrag(PointerInfo pointerInfo)
        {
            if (_isInteractive && _isPressed)
            {
                if(!_isDragged) _isDragged = true;
                _pointer = pointerInfo;
                BeforeOnPointerDragTasks(pointerInfo);
                DoTasks(_onPointerDragTasks, _pointer);
                AfterOnPointerDragTasks(pointerInfo);
                OnPointerDrag?.Invoke(pointerInfo);
            }
        }

        public void PointerEnter(PointerInfo pointerInfo)
        {
            if (_isInteractive && !_isPointerEntered)
            {
                _isPointerEntered = true;
                _pointer = pointerInfo;
                BeforeOnPointerEnterTasks(pointerInfo);
                DoTasks(_onPointerEnterTasks, _pointer);
                AfterOnPointerEnterTasks(pointerInfo);
                OnPointerEnter?.Invoke(pointerInfo);
            }
        }

        public void PointerExit(PointerInfo pointerInfo)
        {
            if (_isInteractive && _isPointerEntered)
            {
                _isPointerEntered = false;
                _pointer = pointerInfo;
                BeforeOnPointerExitTasks(pointerInfo);
                DoTasks(_onPointerExitTasks, _pointer);
                AfterOnPointerExitTasks(pointerInfo);
                OnPointerExit?.Invoke(pointerInfo);
            }
        }

        public void PointerHold(PointerInfo pointerInfo)
        {
            if(_isInteractive && !_isHeld)
            {
                _isHeld = true;
                _pointer = pointerInfo;
                BeforeOnPointerHoldTasks(pointerInfo);
                DoTasks(_onPointerHoldTasks);
                AfterOnPointerHoldTasks(pointerInfo);
                OnPointerHold?.Invoke(pointerInfo);
            }
        }

        private void DoTasks(Task[] tasks, IInfo info = null)
        {
            foreach (var task in tasks)
            {
                task.Do(info);
            }
        }

        private void InjectTasks(DiContainer diContainer, Task[] tasks)
        {
            foreach (var task in tasks)
            {
               diContainer.Inject(task);
            }
        }
        
        protected virtual void BeforeOnPointerDownTasks(PointerInfo pointerInfo){ }
        protected virtual void AfterOnPointerDownTasks(PointerInfo pointerInfo){ }
        protected virtual void BeforeOnPointerUpTasks(PointerInfo pointerInfo){ }
        protected virtual void AfterOnPointerUpTasks(PointerInfo pointerInfo){ }
        protected virtual void BeforeOnPointerMoveTasks(PointerInfo pointerInfo){ }
        protected virtual void AfterOnPointerMoveTasks(PointerInfo pointerInfo){ }
        protected virtual void BeforeOnPointerDragTasks(PointerInfo pointerInfo){ }
        protected virtual void AfterOnPointerDragTasks(PointerInfo pointerInfo){ }
        protected virtual void BeforeOnPointerEnterTasks(PointerInfo pointerInfo){ }
        protected virtual void AfterOnPointerEnterTasks(PointerInfo pointerInfo){ }
        protected virtual void BeforeOnPointerExitTasks(PointerInfo pointerInfo){ }
        protected virtual void AfterOnPointerExitTasks(PointerInfo pointerInfo){ }
        protected virtual void BeforeOnPointerHoldTasks(PointerInfo pointerInfo){ }
        protected virtual void AfterOnPointerHoldTasks(PointerInfo pointerInfo){ }
        protected virtual void AfterOnEnabledTasks(){ }
        protected virtual void BeforeOnDisabledTasks(){ }
        protected virtual void BeforeOnUpdateTasks(){ }
        protected virtual void AfterOnUpdateTasks(){ }
    }
}