using System;
using Sirenix.OdinInspector;
using Sirenix.Utilities;
using UnityEngine;
using Zenject;

namespace Scripts.StateMachineSystem
{
    [Serializable]
    public class Transition
    {
#if UNITY_EDITOR
        [ValueDropdown("@StateUtilities.GetStateTypes()")]
#endif
        [SerializeField] private readonly Type _toState;
        [SerializeField] private readonly Condition[] _conditions = new Condition[0];

        public Type ToState => _toState;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _conditions.ForEach(diContainer.Inject);
        }
        
        public bool IsPossible()
        {
            if (_conditions.Length == 0) return true;
            bool isPossible = false;
            foreach (var condition in _conditions)
            {
                if (!condition.IsFulfilled())
                {
                    if (condition.IsNecessary)
                    {
                        isPossible = false;
                        break;
                    }
                }
                else
                {
                    isPossible = true;
                }
            }
            return isPossible;
        }
    }
}