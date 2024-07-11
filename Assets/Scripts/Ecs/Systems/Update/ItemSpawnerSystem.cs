﻿using Ecs.Components;
using Ecs.Components.Items;
using Ecs.Components.Parameters;
using Ecs.Components.Refs;
using Ecs.Components.Spawner;
using Ecs.Components.Timer;
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
            PrefabComponent<ItemView>,
            DoneComponent,
            ItemFreeSlotsComponent
        >.Exclude<FullComponent> _spawnerFilter;

        public ItemSpawnerSystem(EcsWorld world)
        {
            _world = world;
        }

        public void Run()
        {
            foreach (var spawnerId in _spawnerFilter)
            {
                var slots = _spawnerFilter.Get4(spawnerId).Value;
                if (slots.Count == 0)
                    continue;

                var packedSlot = slots[^1];
                if (!packedSlot.TryUnpack(_world, out var slotEntity))
                    continue;

                var prefab = _spawnerFilter.Get2(spawnerId).Value;
                var spawnPoint = slotEntity.Get<SpawnPointComponent>().Value;
                
                var itemEntity = SpawnItem(prefab, spawnPoint);

                slotEntity.Get<ItemRefComponent>().Value = itemEntity.Pack();

                var spawnerEntity = _spawnerFilter.GetEntity(spawnerId);
                spawnerEntity.Get<ResetTimerComponent>();
            }
        }

        private EcsEntity SpawnItem(ItemView prefab, Transform spawnPoint)
        {
            var item = Object.Instantiate(prefab, spawnPoint.position, Quaternion.identity);

            var itemEntity = _world.NewEntity();
            item.gameObject.Link(itemEntity);

            itemEntity.Get<TransformRefComponent>().Value = item.transform;
            itemEntity.Get<HeightComponent>().Value = item.Height;
            
            
            return itemEntity;
        }
    }
}