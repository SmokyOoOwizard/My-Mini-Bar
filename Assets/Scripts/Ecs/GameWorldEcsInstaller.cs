using Ecs.Core;
using Ecs.Game.Systems.Initialize;
using Ecs.Game.Systems.Update;
using Ecs.Game.Systems.Update.Drinks;
using Ecs.Game.Systems.Update.Items;
using Ecs.Game.Systems.Update.Player;
using Ecs.Game.Systems.Update.Timer;
using Ecs.Game.Systems.Update.TrashCan;
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

            BindSystem<DisableTimerForFullItemSpawnerSystem>();
            BindSystem<ItemSpawnerSystem>();
            BindSystem<SpawnerFreeSlotsSystem>();

            BindSystem<CheckDistanceToItemReceiverSystem>();

            BindSystem<InventoryViewNotifyerSystem>();
            BindSystem<StackInventoryHeightSystem>();


            BindSystem<DrinkSystem>();
            BindSystem<DrinkProgressSystem>();
            BindSystem<FinishDrinkSystem>();

            BindSystem<TrashCanSystem>();
            BindSystem<TrashCanProgressSystem>();
            BindSystem<TrashCanFinishSystem>();


            BindSystem<TweenSystem>();
            BindSystem<DestroySystem>();
        }
    }
}