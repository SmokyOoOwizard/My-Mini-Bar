using Ecs.Core;
using Ecs.Systems.Initialize;
using Ecs.Systems.Update;
using Ecs.Systems.Update.Collectables;
using Ecs.Systems.Update.Drinks;
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
            BindUpdateSystems();
        }

        private void BindInitSystems()
        {
            Container.BindInterfacesAndSelfTo<ViewsInitializeSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<UiInitializeSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<GameStartSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<InputInitializeSystem>().AsSingle();
        }

        private void BindUpdateSystems()
        {
            Container.BindInterfacesAndSelfTo<PlayerMovementSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<CameraTargetSystem>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<PickupItemFromSlotSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<CollectItemSystem>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<ResetTimerSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<TimerSystem>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<ItemSpawnerSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<SpawnerFreeSlotsSystem>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<CheckDistanceToItemReceiverSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DropItemSystem>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<StackInventoryHeightSystem>().AsSingle();
            
            
            Container.BindInterfacesAndSelfTo<DrinkSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DrinkProgressSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<FinishDrinkSystem>().AsSingle();
            
            
            
            
            Container.BindInterfacesAndSelfTo<TweenSystem>().AsSingle();
            Container.BindInterfacesAndSelfTo<DestroySystem>().AsSingle();
        }
    }
}