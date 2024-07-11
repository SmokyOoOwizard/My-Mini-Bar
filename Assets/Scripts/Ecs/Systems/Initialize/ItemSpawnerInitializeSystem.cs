using Ecs.Components;
using Ecs.Components.Items;
using Ecs.Components.Refs;
using Ecs.Components.Spawner;
using Ecs.Components.Timer;
using Ecs.Utils;
using Ecs.Views;
using Leopotam.Ecs;

namespace Ecs.Systems.Initialize
{
    public class ItemSpawnerInitializeSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world;
        private readonly ItemSpawnerView[] _itemSpawnerViews;

        public ItemSpawnerInitializeSystem(
            EcsWorld world,
            ItemSpawnerView[] itemSpawnerViews
        )
        {
            _world = world;
            _itemSpawnerViews = itemSpawnerViews;
        }

        public void Init()
        {
            foreach (var spawnerView in _itemSpawnerViews)
            {
                var spawnerEntity = _world.NewEntity();

                spawnerEntity.Get<ItemSpawnerComponent>();

                spawnerEntity.Get<TransformRefComponent>().Value = spawnerView.transform;

                spawnerEntity.Get<TimerComponent>().Value = spawnerView.timer;

                spawnerEntity.Get<TimerLeftComponent>().Value = spawnerView.timer;

                spawnerEntity.Get<PrefabComponent<ItemView>>().Value = spawnerView.itemPrefab;

                var slotsRef = spawnerEntity.Get<ItemSlotsComponent>().Value;
                var freeSlotsRef = spawnerEntity.Get<ItemFreeSlotsComponent>().Value;

                foreach (var slotView in spawnerView.slots)
                {
                    var slotEntity = _world.NewEntity();
                    
                    slotEntity.Get<TransformRefComponent>().Value = slotView.transform;
                    slotEntity.Get<ItemSlotComponent>();
                    slotEntity.Get<SenderComponent>();
                    slotEntity.Get<SpawnPointComponent>().Value = slotView.spawnPoint;

                    var packedSlot = slotEntity.Pack();

                    slotsRef.Add(packedSlot);
                    freeSlotsRef.Add(packedSlot);
                }
            }
        }
    }
}