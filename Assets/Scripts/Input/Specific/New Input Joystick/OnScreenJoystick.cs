using Sirenix.OdinInspector;
using Sirenix.Serialization;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem.Layouts;
using UnityEngine.InputSystem.OnScreen;
using Zenject;

namespace Sandbox.Input
{
    [ShowOdinSerializedPropertiesInInspector]
    public class OnScreenJoystick : OnScreenControl, IPointerDownHandler, IPointerUpHandler, IDragHandler, 
        ISerializationCallbackReceiver, ISupportsPrefabSerialization
    {

        [SerializeField, InputControl(layout = "Vector2")] private string _controlPath;
        [OdinSerialize] private IJoystick _joystick;

        protected override string controlPathInternal
        {
            get => _controlPath;
            set => _controlPath = value;
        }

        [Inject]
        private void Construct(DiContainer diContainer)
        {
            diContainer.Inject(_joystick);
        }
        
        public void OnPointerDown(PointerEventData eventData)
        {
            _joystick.PointerDown(eventData.position);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _joystick.PointerDrag(eventData.position);
            SendValueToControl(_joystick.Input);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            _joystick.PointerUp();
            SendValueToControl(_joystick.Input);
        }
        
        #region SerializedMonoBehaviourRegion
        
        [SerializeField, HideInInspector]
        private SerializationData serializationData;

        SerializationData ISupportsPrefabSerialization.SerializationData { get { return this.serializationData; } set { this.serializationData = value; } }

        void ISerializationCallbackReceiver.OnAfterDeserialize()
        {
            UnitySerializationUtility.DeserializeUnityObject(this, ref this.serializationData);
        }

        void ISerializationCallbackReceiver.OnBeforeSerialize()
        {
            UnitySerializationUtility.SerializeUnityObject(this, ref this.serializationData);
        }
        
        #endregion
    }
}