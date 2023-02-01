using Scripts.Abstraction;
using Scripts.Input;
using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using Zenject;

namespace Scripts.StateMachineSystem
{
    public class InputEntityMoveState : State
    {
        [OdinSerialize] private IEntity _entity;
        [SerializeField] private Rigidbody _entityRigidbody;
        [SerializeField] private Animator _entityAnimator;
#if UNITY_EDITOR
        [ValueDropdown("@LabelsConfig.GetLabels()")]
#endif
        [OdinSerialize] private ILabel _statsLabel;
        
        [Inject(Id = "Main Camera")] private Camera _camera;
        private int _hashDirectionX;
        private int _hashDirectionZ;
        private SpeedFeature _speed;
        private InputManager _inputManager;
        private Vector3 _moveDirection;
        private Vector2 _inputMoveAxis;
        
        [Inject]
        private void Construct(InputManager inputManager)
        {
            _inputManager = inputManager;
        }

        protected override void Initialize()
        {
            _hashDirectionX = Animator.StringToHash("DirectionX");
            _hashDirectionZ = Animator.StringToHash("DirectionZ");
            _speed = _entity.GetFeature<SpeedFeature>(_statsLabel) as SpeedFeature;
        }

        protected override void EnterActions()
        {
            _inputManager.OnMoveTick += HandleMoveAxisChanged;
        }

        protected override void TickActions()
        {
            
        }

        protected override void FixedTickActions()
        {
            _entityRigidbody.velocity = new Vector3(_speed.Value * _inputMoveAxis.magnitude * _moveDirection.x,
                _entityRigidbody.velocity.y,
                _speed.Value * _inputMoveAxis.magnitude * _moveDirection.z);
        }

        protected override void LateTickActions()
        {
            UpdateMoveDirection();
            _entityRigidbody.rotation = Quaternion.LookRotation(_moveDirection);
            var localDirection = _entityRigidbody.transform.InverseTransformDirection(_moveDirection);
            UpdateAnimatorKeys(new Vector3(localDirection.x * _inputMoveAxis.magnitude, 0f,
                localDirection.z * _inputMoveAxis.magnitude));
        }

        protected override void ExitActions()
        {
            _entityRigidbody.velocity = new Vector3(0f, _entityRigidbody.velocity.y, 0f);
            UpdateAnimatorKeys(Vector3.zero);
            _inputManager.OnMoveTick -= HandleMoveAxisChanged;
        }
        
        private void HandleMoveAxisChanged(InputAxisInfo inputAxisInfo)
        { 
            _inputMoveAxis = inputAxisInfo.Axis;
        }
        
        private void UpdateMoveDirection()
        { 
            Vector3 worldSpaceDirection = new Vector3(_inputMoveAxis.x, 0f, _inputMoveAxis.y);
            Vector3 cameraForward = _camera.transform.forward;
            Vector3 faceDirection = new Vector3(cameraForward.x, 0, cameraForward.z);
            float cameraAngle = Vector3.SignedAngle(Vector3.forward, faceDirection, Vector3.up);
            _moveDirection = (Quaternion.Euler(0, cameraAngle, 0) * worldSpaceDirection).normalized;
        }

        private void UpdateAnimatorKeys(Vector3 localMoveDirection)
        {
            _entityAnimator.SetFloat(_hashDirectionX, localMoveDirection.x);
            _entityAnimator.SetFloat(_hashDirectionZ, localMoveDirection.z);
        }
        
        public override bool Equals(object obj)
        {
            return obj is InputEntityMoveState;
        }

        public override int GetHashCode()
        {
            return nameof(InputEntityMoveState).GetHashCode();
        }
    }
}