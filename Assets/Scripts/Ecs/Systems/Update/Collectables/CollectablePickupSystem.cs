using Ecs.Components;
using Ecs.Components.Collectables;
using Ecs.Components.Parameters;
using Ecs.Components.Refs;
using Ecs.Core;
using Leopotam.Ecs;

namespace Ecs.Systems.Update.Collectables
{
    public class CollectablePickupSystem : IUpdateEcsSystem
    {
        private EcsFilter<PlayerComponent, TransformRefComponent, PickUpDistanceComponent> _playerFilter;
        private EcsFilter<TransformRefComponent, CollectableComponent>.Exclude<CollectedComponent> _itemsFilter;
        
        public void Run()
        {
            if (_playerFilter.IsEmpty())
                return;
            
            var playerTransform = _playerFilter.Get2(0).Value;
            var playerPosition = playerTransform.position;
            var pickUpDistance = _playerFilter.Get3(0).Value;

            var sqrPickUpDistance = pickUpDistance * pickUpDistance;
            
            foreach (var item in _itemsFilter)
            {
                var transform = _itemsFilter.Get1(item).Value;
                var distance = (playerPosition - transform.position).sqrMagnitude;
                
                if (distance > sqrPickUpDistance)
                    continue;
                
                _itemsFilter.GetEntity(item).Get<CollectComponent>();
            }
        }
    }
}