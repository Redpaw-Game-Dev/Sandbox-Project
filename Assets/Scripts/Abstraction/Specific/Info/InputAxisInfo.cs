using UnityEngine;

namespace Scripts.Abstraction
{
    public struct InputAxisInfo : IInfo
    {
        public Vector2 Axis { get; }

        public InputAxisInfo(Vector2 axis)
        {
            Axis = axis;
        }
        
        public override bool Equals(object obj)
        {
            return obj is InputAxisInfo;
        }

        public override int GetHashCode()
        {
            return nameof(InputAxisInfo).GetHashCode();
        } 
    }
}