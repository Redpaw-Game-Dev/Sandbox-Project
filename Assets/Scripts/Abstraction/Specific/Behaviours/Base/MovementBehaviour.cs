using Sirenix.OdinInspector;
using UnityEngine;

namespace Scripts.Abstraction
{
    public abstract class MovementBehaviour : SerializedMonoBehaviour
    {
        public abstract void Move(Vector3 direction);
    }
}