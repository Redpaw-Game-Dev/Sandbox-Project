using UnityEngine;
using Zenject;

namespace Scripts.Loading
{
    public class LoadingManagerInstaller : MonoInstaller
    {
        [SerializeField] protected BaseScenesConfig _baseScenesConfig;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LoadingManager>().FromNew().AsSingle().WithArguments(_baseScenesConfig);
        }
    }
}