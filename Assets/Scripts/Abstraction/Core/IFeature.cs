using System;

namespace Scripts.Abstraction
{
    public interface IFeature : IInfo
    {
        public string Name { get; }
        
        public IFeature GetInstance();
        public void CopyFrom(IFeature attribute);
    }

    public interface IFeature<out T> : IFeature
    {
        public T Value { get; }

        public event Action<T> OnValueChanged;
    }
}