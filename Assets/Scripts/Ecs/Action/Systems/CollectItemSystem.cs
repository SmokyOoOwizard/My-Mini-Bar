using Ecs.Action.Components;
using Ecs.Core;
using Ecs.Game.Components.Inventories;
using Ecs.Utils;
using Ecs.Worlds;
using Leopotam.Ecs;

namespace Ecs.Action.Systems
{
    public class CollectItemSystem : IUpdateEcsSystem
    {
        private readonly GameEcsWorld _world;

        private EcsFilter<
            CollectItemToComponent
        > _itemsFilter;

        public CollectItemSystem(
            GameEcsWorld world
        )
        {
            _world = world;
        }

        public void Run()
        {
            foreach (var actionId in _itemsFilter)
            {
                var collectAction = _itemsFilter.Get1(actionId);
                
                var packedItem = collectAction.Item;
                var packedOwner = collectAction.TargetInventory;
                if (!packedOwner.TryUnpack(_world, out var ownerEntity))
                    continue;

                var inventory = ownerEntity.Get<StackInventoryComponent>().Value;
                inventory.Push(packedItem);
            }
        }
    }
}