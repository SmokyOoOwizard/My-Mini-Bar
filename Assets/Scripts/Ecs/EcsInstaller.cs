using Ecs.Core;
using Zenject;

namespace Ecs
{
    public class EcsInstaller : MonoInstaller<EcsInstaller>
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<EcsBootstrap>().AsSingle().NonLazy();
        }
    }
}