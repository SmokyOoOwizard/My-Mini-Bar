using Ecs.Core;
using Ecs.Systems.Initialize;
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
            
            BindInitSystems();
        }

        private void BindInitSystems()
        {
            Container.BindInterfacesAndSelfTo<UiInitializeSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStartSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputInitializeSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInitializeSystem>().AsSingle();
        }
    }
}