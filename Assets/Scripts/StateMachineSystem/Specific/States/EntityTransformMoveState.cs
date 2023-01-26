using System;
using Scripts.Abstraction;
using Scripts.Input;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Zenject;

namespace Scripts.StateMachineSystem
{
    public class EntityTransformMoveState : IState
    {
        [OdinSerialize] private IEntity _entity;
        [SerializeField] private Transform _entityTransform;
#if UNITY_EDITOR
        [ValueDropdown("@LabelsConfig.GetLabels()")]
#endif
        [OdinSerialize] private ILabel _statsLabel;
        
        [Inject(Id = "Main Camera")] private Camera _camera;
        private SpeedFeature _speed;
        private InputManager _inputManager;
        private Vector3 _moveDirection;
        private Vector2 _inputMoveAxis; 

        public event Action OnEntered;
        public event Action OnExited;

        [Inject]
        private void Construct(InputManager inputManager)
        {
            _inputManager = inputManager;
        }
        
        public void Enter()
        {
            _speed ??= _entity.GetFeature<SpeedFeature>(_statsLabel) as SpeedFeature;
            _inputManager.OnMoveAxisChanged += HandleMoveAxisChanged;
            OnEntered?.Invoke();
        }

        public void Tick()
        {
            UpdateMoveDirection();
            _entityTransform.position = Vector3.MoveTowards(_entityTransform.position,
                _entityTransform.position + _moveDirection * (_speed.Value * _inputMoveAxis.magnitude),
                _speed.Value * _inputMoveAxis.magnitude * Time.deltaTime);
            _entityTransform.rotation = Quaternion.LookRotation(_moveDirection);
        }

        public void Exit()
        {
            _inputManager.OnMoveAxisChanged -= HandleMoveAxisChanged;
            OnExited?.Invoke();
        }
        
        private void HandleMoveAxisChanged(Vector2 vector2)
        { 
            _inputMoveAxis = vector2;
        }
        
        private void UpdateMoveDirection()
        { 
            Vector3 worldSpaceDirection = new Vector3(_inputMoveAxis.x, 0f, _inputMoveAxis.y);
            Vector3 cameraForward = _camera.transform.forward;
            Vector3 faceDirection = new Vector3(cameraForward.x, 0, cameraForward.z);
            float cameraAngle = Vector3.SignedAngle(Vector3.forward, faceDirection, Vector3.up);
            _moveDirection = (Quaternion.Euler(0, cameraAngle, 0) * worldSpaceDirection).normalized;
        }
        
        public override bool Equals(object obj)
        {
            return obj is EntityTransformMoveState;
        }

        public override int GetHashCode()
        {
            return nameof(EntityTransformMoveState).GetHashCode();
        }
    }
}