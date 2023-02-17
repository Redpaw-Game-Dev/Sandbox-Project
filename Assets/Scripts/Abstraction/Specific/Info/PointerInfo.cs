using UnityEngine;

namespace Sandbox.Abstraction
{
    public struct PointerInfo : IInfo
    {
        public bool Contact;
        public int Id;
        public Vector2 Position;
        public Vector3 WorldPosition;
        public Vector2 Delta;
        
                
        public override bool Equals(object obj)
        {
            return obj is PointerInfo;
        }

        public override int GetHashCode()
        {
            return nameof(PointerInfo).GetHashCode();
        }
    }
}