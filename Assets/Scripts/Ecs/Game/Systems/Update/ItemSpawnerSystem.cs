using Ecs.Core;
using Ecs.Game.Components;
using Ecs.Game.Components.Items;
using Ecs.Game.Components.Spawner;
using Ecs.Game.Components.Timer;
using Ecs.Utils;
using Ecs.Views.Impl;
using Ecs.Worlds;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Game.Systems.Update
{
    public class ItemSpawnerSystem : IUpdateEcsSystem
    {
        private readonly GameEcsWorld _world;

        private EcsFilter<
            ItemSpawnerComponent,
            PrefabComponent<ItemView>,
            DoneComponent,
            ItemFreeSlotsComponent
        >.Exclude<FullComponent> _spawnerFilter;

        public ItemSpawnerSystem(GameEcsWorld world)
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
            item.Init(itemEntity, _world);
            
            return itemEntity;
        }
    }
}