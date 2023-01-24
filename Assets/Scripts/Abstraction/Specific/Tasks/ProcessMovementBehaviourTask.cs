﻿using UnityEngine;
using Zenject;

namespace Scripts.Abstraction
{
    public class ProcessMovementBehaviourTask : Task
    {
        [SerializeField] private MovementBehaviour _movementBehaviour;

        [Inject(Id = "Main Camera")] private Camera _camera;
        
        public override void Do(IInfo info = null)
        {
            if (info is Vector2Info vector2Info)
            {
                Vector2 inputDirection = vector2Info.Vector;
                Vector3 worldSpaceDirection = new Vector3(inputDirection.x, 0f, inputDirection.y);
                Vector3 cameraForward = _camera.transform.forward;
                Vector3 faceDirection = new Vector3(cameraForward.x, 0, cameraForward.z);
                float cameraAngle = Vector3.SignedAngle(Vector3.forward, faceDirection, Vector3.up);
                Vector3 localSpaceDirection = (Quaternion.Euler(0, cameraAngle, 0) * worldSpaceDirection).normalized;
                _movementBehaviour.Move(localSpaceDirection);
            }
        }
    }
}