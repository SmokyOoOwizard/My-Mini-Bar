using Ecs.Components;
using Ecs.Components.Collectables;
using Ecs.Core;
using Ecs.Utils;
using Leopotam.Ecs;

namespace Ecs.Systems.Update.Collectables
{
    public class CollectCollectableSystem : IUpdateEcsSystem
    {
        private EcsFilter<PlayerComponent, StackInventoryComponent> _playerFilter;
        private EcsFilter<CollectableComponent, CollectComponent> _itemsFilter;
        
        public void Run()
        {
            if (_playerFilter.IsEmpty())
                return;
            
            var inventory = _playerFilter.Get2(0).Value;
            
            foreach (var item in _itemsFilter)
            {
                var itemEntity = _itemsFilter.GetEntity(item);
                
                var itemId = itemEntity.Pack();
                
                inventory.Push(itemId);
                
                itemEntity.Del<CollectComponent>();
                itemEntity.Get<CollectedComponent>();
            }
        }
    }
}