using UnityEngine;
using Zenject;

namespace Scripts.ObjectsManagement
{
    public class ObjectsManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<ObjectsManager>().AsSingle();
        }
    }
}