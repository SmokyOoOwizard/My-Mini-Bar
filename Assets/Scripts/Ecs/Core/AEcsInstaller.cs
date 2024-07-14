using Leopotam.Ecs;
using Zenject;

namespace Ecs.Core
{
    public abstract class AEcsInstaller<TWorld> : MonoInstaller where TWorld : EcsWorld
    {
        protected void BindSystem<TSystem>() where TSystem : IEcsSystem
        {
            Container.BindInterfacesAndSelfTo<TSystem>().AsSingle();
            Container.Bind<SystemInstaller>().To<SystemInstaller<TWorld, TSystem>>().AsSingle();
        }
    }
}