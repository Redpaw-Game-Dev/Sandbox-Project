using System;
using Scripts.Abstraction;
using UnityEngine;

namespace Scripts.Input
{
    public interface IInteractive
    {
        public bool IsInteractive { get; }
        public float HoldThresholdTime { get; }
        public bool IsPressed { get; }
        public float PressTime { get; }
        public bool IsHeld { get; }
        public bool IsPointerEntered { get; }
        public bool IsDragged { get; }
        public PointerInfo Pointer { get; }
        public Transform Transform { get; }
        
        public event Action<PointerInfo> OnPointerDown;
        public event Action<PointerInfo> OnPointerUp;
        public event Action<PointerInfo> OnPointerMove;
        public event Action<PointerInfo> OnPointerEnter;
        public event Action<PointerInfo> OnPointerExit;
        public event Action<PointerInfo> OnPointerDrag;
        public event Action<PointerInfo> OnPointerHold;
        public event Action OnEnabled;
        public event Action OnDisabled;
        
        public void PointerDown(PointerInfo pointerInfo);
        public void PointerUp(PointerInfo pointerInfo);
        public void PointerMove(PointerInfo pointerInfo);
        public void PointerDrag(PointerInfo pointerInfo);
        public void PointerEnter(PointerInfo pointerInfo);
        public void PointerExit(PointerInfo pointerInfo);
        public void PointerHold(PointerInfo pointerInfo);
    }
}