using Ecs.Core;
using Ecs.Game.Components.Inventories;
using Ecs.Game.Components.Parameters;
using Ecs.Utils;
using Ecs.Worlds;
using Leopotam.Ecs;

namespace Ecs.Game.Systems.Update
{
    public class StackInventoryHeightSystem : IUpdateEcsSystem
    {
        private readonly GameEcsWorld _world;

        private EcsFilter<
            StackInventoryComponent,
            InventoryUpdatedComponent,
            StackInventoryHeightComponent
        > _inventoryFilter;

       

        public StackInventoryHeightSystem(GameEcsWorld world)
        {
            _world = world;
        }

        public void Run()
        {
            foreach (var inventoryId in _inventoryFilter)
            {
                ref var height = ref _inventoryFilter.Get3(inventoryId).Value;
                height = 0;

                var inventory = _inventoryFilter.Get1(inventoryId).Value;
                foreach (var packedItem in inventory)
                {
                    if (!packedItem.TryUnpack(_world, out var itemEntity))
                        continue;
                    
                    height += itemEntity.Get<HeightComponent>().Value;
                }

                var inventoryEntity = _inventoryFilter.GetEntity(inventoryId);
                inventoryEntity.Del<InventoryUpdatedComponent>();
            }
        }
    }
}