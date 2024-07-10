using System.Collections.Generic;
using Ecs.Components;
using Ecs.Components.Collectables;
using Ecs.Components.Swapner;
using Ecs.Components.Timer;
using Ecs.Core;
using Ecs.Utils;
using Leopotam.Ecs;

namespace Ecs.Systems.Update.Collectables
{
    public class CollectFromSpawnerPickupSystem : IUpdateEcsSystem
    {
        private readonly EcsWorld _world;

        private EcsFilter<
            CollectableComponent,
            CollectComponent,
            InSpawnerComponent,
            SpawnerIdComponent
        > _itemsFilter;

        public CollectFromSpawnerPickupSystem(EcsWorld world)
        {
            _world = world;
        }

        public void Run()
        {
            foreach (var itemId in _itemsFilter)
            {
                var spawnerId = _itemsFilter.Get4(itemId).Value;
                if (!spawnerId.TryUnpack(_world, out var spawner))
                    continue;

                if (!spawner.Has<InventoryComponent<List<EntityId>>>())
                    continue;

                var inventory = spawner.Get<InventoryComponent<List<EntityId>>>().Value;

                var itemEntity = _itemsFilter.GetEntity(itemId);
                var packedItem = itemEntity.Pack();
                inventory.Remove(packedItem);

                itemEntity.Del<InSpawnerComponent>();
                itemEntity.Del<SpawnerIdComponent>();

                spawner.Get<ResetTimerComponent>();
            }
        }
    }
}