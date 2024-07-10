using System.Collections.Generic;
using Ecs.Components;
using Ecs.Components.Collectables;
using Ecs.Components.Parameters;
using Ecs.Components.Refs;
using Ecs.Components.Swapner;
using Ecs.Core;
using Ecs.Utils;
using Ecs.Views;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems.Update
{
    public class ItemSpawnerSystem : IUpdateEcsSystem
    {
        private readonly EcsWorld _world;

        private EcsFilter<
            ItemSpawnerComponent,
            SpawnPointComponent,
            PrefabComponent<ItemView>,
            DoneComponent,
            InventoryComponent<List<EntityId>>
        > _spawnerFilter;

        public ItemSpawnerSystem(EcsWorld world)
        {
            _world = world;
        }

        public void Run()
        {
            foreach (var entityId in _spawnerFilter)
            {
                var inventory = _spawnerFilter.Get5(entityId).Value;
                if (inventory.Count > 0)
                    continue;

                var prefab = _spawnerFilter.Get3(entityId).Value;

                var spawnPoint = _spawnerFilter.Get2(entityId).Value;

                var item = Object.Instantiate(prefab, spawnPoint.position, Quaternion.identity);

                var itemEntity = _world.NewEntity();
                item.gameObject.Link(itemEntity);
                
                var packedItem = itemEntity.Pack();
                inventory.Add(packedItem);

                itemEntity.Get<TransformRefComponent>().Value = item.transform;
                itemEntity.Get<CollectableComponent>();
                itemEntity.Get<InSpawnerComponent>();
                itemEntity.Get<HeightComponent>().Value = item.Height;

                var spawner = _spawnerFilter.GetEntity(entityId);
                itemEntity.Get<SpawnerIdComponent>().Value = spawner.Pack();
            }
        }
    }
}