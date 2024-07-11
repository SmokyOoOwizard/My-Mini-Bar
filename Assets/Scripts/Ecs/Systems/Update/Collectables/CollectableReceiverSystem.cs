using System.Collections.Generic;
using Ecs.Components;
using Ecs.Components.Items;
using Ecs.Components.Parameters;
using Ecs.Components.Refs;
using Ecs.Core;
using Ecs.Utils;
using Leopotam.Ecs;

namespace Ecs.Systems.Update.Collectables
{
    public class ItemReceiverSystem : IUpdateEcsSystem
    {
        private readonly EcsWorld _world;

        private EcsFilter<
            PlayerComponent,
            TransformRefComponent,
            InventoryComponent<Stack<EntityId>>
        > _playerFilter;

        
        private EcsFilter<
            TransformRefComponent,
            PickUpDistanceComponent,
            ItemSlotComponent,
            ReceiverComponent
        >.Exclude<ItemRefComponent, DoneComponent> _slotsFilter;

        public ItemReceiverSystem(EcsWorld world)
        {
            _world = world;
        }

        public void Run()
        {
            if (_playerFilter.IsEmpty())
                return;

            var playerTransform = _playerFilter.Get2(0).Value;
            var playerInventory = _playerFilter.Get3(0).Value;


            foreach (var slotId in _slotsFilter)
            {
                if (playerInventory.Count == 0)
                    return;

                var pickUpDistance = _slotsFilter.Get2(slotId).Value;
                var sqrDistance = pickUpDistance * pickUpDistance;

                var receiverTransform = _slotsFilter.Get1(slotId).Value;
                var diff = receiverTransform.position - playerTransform.position;
                if (diff.sqrMagnitude > sqrDistance)
                    continue;

                var receiverInventory = _slotsFilter.Get2(slotId).Value;

                var item = playerInventory.Pop();

                var slotEntity = _slotsFilter.GetEntity(slotId);
                slotEntity.Get<ItemRefComponent>().Value = item;
                slotEntity.Get<DoneComponent>();
            }
        }
    }
}