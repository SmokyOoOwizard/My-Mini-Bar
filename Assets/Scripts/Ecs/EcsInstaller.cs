using Ecs.Core;
using Ecs.Worlds;
using Zenject;

namespace Ecs
{
    public class EcsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameEcsWorld>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<ActionEcsWorld>().AsSingle().NonLazy();
            Container.BindInterfacesAndSelfTo<EcsBootstrap>().AsSingle().NonLazy();
        }
    }
}