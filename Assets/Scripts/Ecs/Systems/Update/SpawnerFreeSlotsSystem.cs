using Ecs.Components;
using Ecs.Components.Items;
using Ecs.Components.Spawner;
using Ecs.Core;
using Ecs.Utils;
using Ecs.Worlds;
using Leopotam.Ecs;

namespace Ecs.Systems.Update
{
    public class SpawnerFreeSlotsSystem : IUpdateEcsSystem
    {
        private readonly GameEcsWorld _world;

        private EcsFilter<
            ItemSpawnerComponent,
            ItemSlotsComponent,
            ItemFreeSlotsComponent
        > _spawnerFilter;

        public SpawnerFreeSlotsSystem(GameEcsWorld world)
        {
            _world = world;
        }

        public void Run()
        {
            foreach (var spawnerId in _spawnerFilter)
            {
                var slots = _spawnerFilter.Get2(spawnerId).Value;
                var freeSlots = _spawnerFilter.Get3(spawnerId).Value;
                freeSlots.Clear();

                foreach (var slotId in slots)
                {
                    if (!slotId.TryUnpack(_world, out var slotEntity))
                        continue;
                    
                    if(!slotEntity.Has<ItemRefComponent>())
                        freeSlots.Add(slotId);
                }

                var spawnerEntity = _spawnerFilter.GetEntity(spawnerId);
                var full = freeSlots.Count == 0;
                if (full)
                    spawnerEntity.Get<FullComponent>();
                else
                    spawnerEntity.Del<FullComponent>();
            }
        }
    }
}