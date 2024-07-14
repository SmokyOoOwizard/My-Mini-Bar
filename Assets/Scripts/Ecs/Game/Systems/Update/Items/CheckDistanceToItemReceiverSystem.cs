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
    public class CheckDistanceToItemReceiverSystem : IUpdateEcsSystem
    {
        private readonly GameEcsWorld _world;
        private readonly ActionEcsWorld _actionWorld;

        private EcsFilter<
            StackInventoryComponent,
            TransformRefComponent
        > _stackInventoriesFilter;

        private EcsFilter<
            TransformRefComponent,
            PickUpDistanceComponent,
            ItemSlotComponent,
            ReceiverComponent,
            ItemFilterComponent
        >.Exclude<ItemRefComponent, DoneComponent> _slotsFilter;

        public CheckDistanceToItemReceiverSystem(
            GameEcsWorld world,
            ActionEcsWorld actionWorld
        )
        {
            _world = world;
            _actionWorld = actionWorld;
        }

        public void Run()
        {
            foreach (var inventoryId in _stackInventoriesFilter)
            {
                var inventory = _stackInventoriesFilter.Get1(inventoryId).Value;
                if (inventory.Count == 0)
                    continue;

                var inventoryTransform = _stackInventoriesFilter.Get2(inventoryId).Value;
                var inventoryPosition = inventoryTransform.position;
                var packedItem = inventory.Peek();

                if (!packedItem.TryUnpack(_world, out var itemEntity))
                    continue;

                var itemType = itemEntity.Get<ItemTypeComponent>().Value;

                foreach (var slotId in _slotsFilter)
                {
                    var filter = _slotsFilter.Get5(slotId).Value;
                    if (filter != EItemFilter.Any && !filter.Match(itemType))
                        continue;

                    var slotTransform = _slotsFilter.Get1(slotId).Value;

                    var pickUpDistance = _slotsFilter.Get2(slotId).Value;
                    var sqrDistance = pickUpDistance * pickUpDistance;

                    var diff = inventoryPosition - slotTransform.position;
                    if (diff.sqrMagnitude > sqrDistance)
                        continue;

                    var inventoryEntity = _stackInventoriesFilter.GetEntity(inventoryId);
                    var slotEntity = _slotsFilter.GetEntity(slotId);

                    var packedInventory = inventoryEntity.Pack();
                    var packedSlot = slotEntity.Pack();
                    _actionWorld.DropItemTo(packedInventory, packedSlot);
                    break;
                }
            }
        }
    }
}