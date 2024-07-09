using Ecs.Core;
using Leopotam.Ecs;
using Zenject;

namespace Ecs
{
    public class EcsInstaller : MonoInstaller<EcsInstaller>
    {
        public override void InstallBindings()
        {
            Container.Bind<EcsWorld>().To<BestEcsWorld>().AsSingle();
            Container.BindInterfacesAndSelfTo<EcsBootstrap>().AsSingle().NonLazy();
        }
    }
}