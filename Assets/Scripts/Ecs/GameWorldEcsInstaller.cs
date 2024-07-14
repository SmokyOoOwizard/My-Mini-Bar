using Ecs.Core;
using Ecs.Systems.Initialize;
using Ecs.Systems.Update;
using Ecs.Systems.Update.Collectables;
using Ecs.Systems.Update.Drinks;
using Ecs.Worlds;

namespace Ecs
{
    public class GameWorldEcsInstaller : AEcsInstaller<GameEcsWorld>
    {
        public override void InstallBindings()
        {
            BindInitSystems();
            BindUpdateSystems();
        }

        private void BindInitSystems()
        {
            BindSystem<ViewsInitializeSystem>();
            BindSystem<UiInitializeSystem>();
            BindSystem<GameStartSystem>();
            BindSystem<InputInitializeSystem>();
        }

        private void BindUpdateSystems()
        {
            BindSystem<PlayerMovementSystem>();
            BindSystem<PlayerRotationSystem>();
            BindSystem<CameraTargetSystem>();

            BindSystem<PickupItemFromSlotSystem>();

            BindSystem<ResetTimerSystem>();
            BindSystem<TimerSystem>();

            BindSystem<ItemSpawnerSystem>();
            BindSystem<SpawnerFreeSlotsSystem>();

            BindSystem<CheckDistanceToItemReceiverSystem>();

            BindSystem<StackInventoryHeightSystem>();


            BindSystem<DrinkSystem>();
            BindSystem<DrinkProgressSystem>();
            BindSystem<FinishDrinkSystem>();


            BindSystem<TweenSystem>();
            BindSystem<DestroySystem>();
        }
    }
}