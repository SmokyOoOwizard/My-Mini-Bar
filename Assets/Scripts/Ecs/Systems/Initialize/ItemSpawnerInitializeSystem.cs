using Ecs.Components;
using Ecs.Components.Refs;
using Ecs.Components.Timer;
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
                var item = _world.NewEntity();

                item.Get<ItemSpawnerComponent>();

                item.Get<TransformRefComponent>().Value = spawnerView.transform;

                item.Get<TimerComponent>().Value = spawnerView.Timer;

                item.Get<TimerLeftComponent>().Value = spawnerView.Timer;

                item.Get<SpawnPointComponent>().Value = spawnerView.spawnPoint;
                
                item.Get<PrefabComponent<ItemView>>().Value = spawnerView.itemPrefab;

                item.Get<StackInventoryComponent>();
            }
        }
    }
}