using UnityEngine;

namespace Scripts.Abstraction
{
    public struct Vector2Info : IInfo
    {
        public Vector2 Vector { get; }

        public Vector2Info(Vector2 vector)
        {
            Vector = vector;
        }
        
        public override bool Equals(object obj)
        {
            return obj is Vector2Info;
        }

        public override int GetHashCode()
        {
            return nameof(Vector2Info).GetHashCode();
        } 
    }
}