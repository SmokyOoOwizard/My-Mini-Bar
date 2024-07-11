using Ecs.Components;
using Ecs.Components.Inventories;
using Ecs.Components.Items;
using Ecs.Components.Parameters;
using Ecs.Core;
using Ecs.Utils;
using Leopotam.Ecs;

namespace Ecs.Systems.Update.Collectables
{
    public class StackInventoryHeightSystem : IUpdateEcsSystem
    {
        private readonly EcsWorld _world;

        private EcsFilter<
            StackInventoryComponent,
            InventoryUpdatedComponent,
            StackInventoryHeightComponent
        > _inventoryFilter;

       

        public StackInventoryHeightSystem(EcsWorld world)
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