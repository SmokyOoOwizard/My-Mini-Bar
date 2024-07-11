using Ecs.Components;
using Ecs.Components.Items;
using Ecs.Components.Parameters;
using Ecs.Components.Refs;
using Ecs.Core;
using Ecs.Utils;
using Leopotam.Ecs;

namespace Ecs.Systems.Update.Collectables
{
    public class CheckDistanceToItemReceiverSystem : IUpdateEcsSystem
    {
        private readonly EcsWorld _world;

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

        public CheckDistanceToItemReceiverSystem(EcsWorld world)
        {
            _world = world;
        }

        public void Run()
        {
            foreach (var inventoryId in _stackInventoriesFilter)
            {
                var inventory = _stackInventoriesFilter.Get1(inventoryId).Value;
                var inventoryTransform = _stackInventoriesFilter.Get2(inventoryId).Value;
                var inventoryPosition = inventoryTransform.position;
                var packedItem = inventory.Peek();

                if (!packedItem.TryUnpack(_world, out var itemEntity))
                    continue;

                var itemType = itemEntity.Get<ItemTypeComponent>().Value;

                foreach (var slotId in _slotsFilter)
                {
                    var filter = _slotsFilter.Get5(slotId).Value;
                    if (filter != EItemFilter.Any || !filter.Match(itemType))
                        continue;

                    var slotTransform = _slotsFilter.Get1(slotId).Value;

                    var pickUpDistance = _slotsFilter.Get2(slotId).Value;
                    var sqrDistance = pickUpDistance * pickUpDistance;

                    var diff = inventoryPosition - slotTransform.position;
                    if (diff.sqrMagnitude > sqrDistance)
                        continue;

                    var inventoryEntity = _stackInventoriesFilter.GetEntity(inventoryId);
                    var slotEntity = _slotsFilter.GetEntity(slotId);
                    inventoryEntity.Get<DropItemToComponent>().Value = slotEntity.Pack();
                    break;
                }
            }
        }
    }
}