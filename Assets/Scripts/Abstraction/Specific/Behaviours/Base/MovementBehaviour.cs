using Sirenix.OdinInspector;
using UnityEngine;

namespace Sandbox.Abstraction
{
    public abstract class MovementBehaviour : SerializedMonoBehaviour
    {
        public abstract void Move(Vector3 direction);
    }
}