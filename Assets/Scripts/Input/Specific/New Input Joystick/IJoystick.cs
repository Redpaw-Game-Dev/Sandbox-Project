using UnityEngine;

namespace Sandbox.Input
{
    public interface IJoystick
    {
        public Vector2 Input { get; }

        public void PointerDown(Vector2 pointerPosition);
        public void PointerDrag(Vector2 pointerPosition);
        public void PointerUp();
    }
}