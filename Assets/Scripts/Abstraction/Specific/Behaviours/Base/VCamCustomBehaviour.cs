using System;
using Cinemachine;
using Sandbox.ObjectsManagement;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;

namespace Sandbox.Abstraction
{
    public class VCamCustomBehaviour : SerializedMonoBehaviour
    {
        private readonly int _priorityActivated = 10;
        private readonly int _priorityDeactivated = -10;
        
        [SerializeField] protected CinemachineVirtualCameraBase _vCam;
        [SerializeField] protected bool _setFollowTarget;
        [SerializeField] protected bool _setLookAtTarget;
        [SerializeField] protected string _targetTransformId;

        protected Transform _targetTransform;
        protected bool _isActive;

        public int Priority => _vCam.m_Priority;

        public event Action<VCamCustomBehaviour> OnActivated;
        public event Action<VCamCustomBehaviour> OnDeactivated;
                
        [Inject]
        protected void Construct(ObjectsManager objectsManager)
        {
            _targetTransform = objectsManager.GetObject<Transform>(_targetTransformId, GetTransformCallback);
        }

        public void Activate()
        {
            if(_isActive) return;
            _isActive = true;
            SetPriority(_priorityActivated);
            OnActivated?.Invoke(this);
        }
        
        public void Deactivate()
        {
            if(!_isActive) return;
            _isActive = false;
            SetPriority(_priorityDeactivated);
            OnDeactivated?.Invoke(this);
        }
        
        private void SetPriority(int priority)
        {
            _vCam.Priority = priority;
        }
        
        private void GetTransformCallback(Object obj)
        {
            _targetTransform = (Transform)obj;
            if (_targetTransform != null)
            {
                if (_setFollowTarget) _vCam.Follow = _targetTransform;
                if (_setLookAtTarget) _vCam.LookAt = _targetTransform;
            }
        }
    }
}