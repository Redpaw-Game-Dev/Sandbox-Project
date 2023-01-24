using System;
using UnityEngine;

namespace Scripts.Abstraction
{
    public abstract class FloatFeature : IFeature<float>
    {
        [SerializeField] protected float _value;
        
        public virtual string Name => "Float";

        public float Value
        {
            get => _value;
            set
            {
                _value = value;
                OnValueChanged?.Invoke(_value);
            }
        }

        public event Action<float> OnValueChanged;

        public IFeature GetInstance() => this;

        public void CopyFrom(IFeature feature)
        {
            if (feature is FloatFeature floatFeature)
            {
                _value = floatFeature._value;
            }
        }
        
        public override bool Equals(object obj)
        {
            return obj is FloatFeature;
        }

        public override int GetHashCode()
        {
            return nameof(FloatFeature).GetHashCode();
        }
    }
}