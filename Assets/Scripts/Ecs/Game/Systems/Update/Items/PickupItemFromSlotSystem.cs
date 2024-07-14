using Ecs.Core;
using Ecs.Game.Components;
using Ecs.Game.Components.Inventories;
using Ecs.Game.Components.Items;
using Ecs.Game.Components.Parameters;
using Ecs.Game.Components.Refs;
using Ecs.Utils;
using Ecs.Worlds;
using Leopotam.Ecs;

namespace Ecs.Game.Systems.Update.Items
{
    public class PickupItemFromSlotSystem : IUpdateEcsSystem
    {
        private readonly ActionEcsWorld _actionWorld;

        private EcsFilter<
            StackInventoryComponent,
            TransformRefComponent,
            PickUpDistanceComponent,
            MaxItemsComponent
        > _inventoriesFilter;

        private EcsFilter<
            TransformRefComponent,
            ItemSlotComponent,
            ItemRefComponent,
            SenderComponent
        > _itemSlotsFilter;

        public PickupItemFromSlotSystem(
            ActionEcsWorld actionWorld
        )
        {
            _actionWorld = actionWorld;
        }

        public void Run()
        {
            foreach (var inventoryId in _inventoriesFilter)
            {
                var inventoryTransform = _inventoriesFilter.Get2(inventoryId).Value;
                var inventoryPosition = inventoryTransform.position;
                var pickUpDistance = _inventoriesFilter.Get3(inventoryId).Value;

                var maxItems = _inventoriesFilter.Get4(inventoryId).Value;
                var inventory = _inventoriesFilter.Get1(inventoryId).Value;
                if (inventory.Count >= maxItems)
                    continue;

                var sqrPickUpDistance = pickUpDistance * pickUpDistance;

                foreach (var slotId in _itemSlotsFilter)
                {
                    var transform = _itemSlotsFilter.Get1(slotId).Value;
                    var distance = (inventoryPosition - transform.position).sqrMagnitude;

                    if (distance > sqrPickUpDistance)
                        continue;

                    var slotEntity = _itemSlotsFilter.GetEntity(slotId);

                    var packedItem = _itemSlotsFilter.Get3(slotId).Value;
                    
                    slotEntity.Del<ItemRefComponent>();
                    
                    var packedInventory = _inventoriesFilter.GetEntity(inventoryId).Pack();
                    _actionWorld.CollectItemTo(packedItem, packedInventory);
                }
            }
        }
    }
}