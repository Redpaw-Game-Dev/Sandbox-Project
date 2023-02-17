using UnityEngine;
using Zenject;

namespace Sandbox.Loading
{
    public class LoadingManagerInstaller : MonoInstaller
    {
        [SerializeField] protected BaseScenesConfig _baseScenesConfig;
        
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<LoadingManager>().AsSingle().WithArguments(_baseScenesConfig);
        }
    }
}