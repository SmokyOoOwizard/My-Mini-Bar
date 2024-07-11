using Ecs.Components;
using Ecs.Components.Items;
using Ecs.Components.Parameters;
using Ecs.Components.Refs;
using Ecs.Core;
using Ecs.Utils;
using Leopotam.Ecs;

namespace Ecs.Systems.Update.Collectables
{
    public class PickupItemFromSlotSystem : IUpdateEcsSystem
    {
        private readonly EcsWorld _world;
        
        private EcsFilter<PlayerComponent, TransformRefComponent, PickUpDistanceComponent> _playerFilter;

        private EcsFilter<
            TransformRefComponent,
            ItemSlotComponent,
            ItemRefComponent,
            SenderComponent
        > _itemSlotsFilter;

        public PickupItemFromSlotSystem(EcsWorld world)
        {
            _world = world;
        }

        public void Run()
        {
            if (_playerFilter.IsEmpty())
                return;
            
            var playerTransform = _playerFilter.Get2(0).Value;
            var playerPosition = playerTransform.position;
            var pickUpDistance = _playerFilter.Get3(0).Value;

            var sqrPickUpDistance = pickUpDistance * pickUpDistance;
            
            foreach (var slotId in _itemSlotsFilter)
            {
                var transform = _itemSlotsFilter.Get1(slotId).Value;
                var distance = (playerPosition - transform.position).sqrMagnitude;
                
                if (distance > sqrPickUpDistance)
                    continue;

                var slotEntity = _itemSlotsFilter.GetEntity(slotId);
                

                var packedItem = _itemSlotsFilter.Get3(slotId).Value;
                if (!packedItem.TryUnpack(_world, out var itemEntity))
                    continue;
                
                slotEntity.Del<ItemRefComponent>();

                itemEntity.Get<CollectComponent>();
            }
        }
    }
}