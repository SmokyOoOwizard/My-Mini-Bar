using Ecs.Components;
using Ecs.Components.Inventories;
using Ecs.Components.Items;
using Ecs.Components.Parameters;
using Ecs.Components.Refs;
using Ecs.Core;
using Ecs.Utils;
using Ecs.Worlds;
using Leopotam.Ecs;

namespace Ecs.Systems.Update.Collectables
{
    public class PickupItemFromSlotSystem : IUpdateEcsSystem
    {
        private readonly GameEcsWorld _world;

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

        public PickupItemFromSlotSystem(GameEcsWorld world)
        {
            _world = world;
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
                    if (!packedItem.TryUnpack(_world, out var itemEntity))
                        continue;

                    slotEntity.Del<ItemRefComponent>();

                    var packedInventory = _inventoriesFilter.GetEntity(inventoryId).Pack();
                    itemEntity.Get<CollectItemToComponent>().Value = packedInventory;
                }
            }
        }
    }
}