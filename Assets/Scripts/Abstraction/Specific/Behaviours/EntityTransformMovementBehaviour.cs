using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;

namespace Sandbox.Abstraction
{
    public class EntityTransformMovementBehaviour : MovementBehaviour
    {
        [OdinSerialize] private IEntity _entity;
        [SerializeField] private Transform _entityTransform;
#if UNITY_EDITOR
        [ValueDropdown("@LabelsConfig.GetLabels()")]
#endif
        [OdinSerialize] private ILabel _statsLabel;

        private SpeedFeature _speed;

        private void Awake()
        {
            _speed = _entity.GetFeature<SpeedFeature>(_statsLabel) as SpeedFeature;
        }

        public override void Move(Vector3 direction)
        {
            _entityTransform.position = Vector3.MoveTowards(_entityTransform.position,
                _entityTransform.position + direction * _speed.Value, Time.deltaTime * _speed.Value);
            _entityTransform.rotation = Quaternion.LookRotation(direction);
        }
    }
}