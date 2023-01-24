using Zenject;

namespace Scripts.Input
{
    public class InputManagerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<InputManager>().AsSingle();
        }
    }
}