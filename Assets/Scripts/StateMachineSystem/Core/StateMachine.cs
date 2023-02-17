using System;
using System.Collections.Generic;
using System.Linq;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using Sirenix.Utilities;
using UnityEngine;
using Zenject;

namespace Sandbox.StateMachineSystem
{
    public class StateMachine : SerializedMonoBehaviour
    {
        [OdinSerialize, PropertyOrder(0)] private IState[] _states = new IState[0];
#if UNITY_EDITOR
        [ValueDropdown("@StateUtilities.GetStateTypes()")]
#endif
        [SerializeField] private Type _initialState;
        [OdinSerialize, DictionaryDrawerSettings(IsReadOnly = true, KeyLabel = "State", ValueLabel = "Transition Array"),
         PropertyOrder(2)]
        private Dictionary<Type, Transition[]> _transitions = new Dictionary<Type, Transition[]>();
        [SerializeField] private Transition[] _anyTransitions = new Transition[0];

        private IState _currentState;

        public IState CurrentState => _currentState;

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            _states.ForEach(diContainer.Inject);
            _transitions.ForEach(pair => pair.Value.ForEach(diContainer.Inject));
            _anyTransitions.ForEach(diContainer.Inject);
        }

        private void Start()
        {
            SetState(GetStateByType(_initialState));
        }

        private void Update()
        {
            _currentState?.Tick();
        }

        private void FixedUpdate()
        {
            _currentState.FixedTick();
        }

        private void LateUpdate()
        {
            _currentState.LateTick();
        }

        private void SetState(IState state)
        {
            if (state != null && !ReferenceEquals(_currentState, state) && _states.Contains(state))
            {
                _currentState?.Exit();
                _currentState = state;
                _currentState?.Enter();
            }
        }

        private IState GetStateByType(Type stateType) => _states.FirstOrDefault(state => state.GetType() == stateType);

        private Transition TryGetTransition()
        {
            Transition result = _anyTransitions.FirstOrDefault(transition => transition.IsPossible());
            if (result == null && _currentState != null)
            {
                result = _transitions[_currentState.GetType()].FirstOrDefault(transition => transition.IsPossible());
            }
            return result;
        }

        public void TryChangeState()
        {
            var transition = TryGetTransition();
            if (transition != null)
            {
                SetState(GetStateByType(transition.ToState));
            }
        }
        
#if UNITY_EDITOR
        [OdinSerialize, ValueDropdown("@StateUtilities.GetStateTypes()"),
         InlineButton("RemoveTransitionThroughEditor", "Remove"),
         InlineButton("AddTransitionThroughEditor", "Add"),
         PropertyOrder(1), HideInPlayMode]
        private Type _editorState;
        
        private void AddTransitionThroughEditor()
        {
            if (!_transitions.ContainsKey(_editorState))
            {
                _transitions.Add(_editorState, new Transition[0]);
            }
        }
        
        private void RemoveTransitionThroughEditor()
        {
            if (_transitions.ContainsKey(_editorState))
            {
                if (!_transitions.ContainsKey(_editorState))
                {
                    _transitions.Remove(_editorState);
                }
            }
        }
#endif
    }
}