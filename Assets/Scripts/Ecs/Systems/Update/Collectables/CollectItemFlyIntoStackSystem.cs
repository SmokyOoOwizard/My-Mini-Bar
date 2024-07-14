using Ecs.Components;
using Ecs.Components.Inventories;
using Ecs.Components.Parameters;
using Ecs.Core;
using Ecs.Utils;
using Ecs.Worlds;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems.Update.Collectables
{
    public class CollectItemFlyIntoStackSystem : IUpdateEcsSystem
    {
        private readonly GameEcsWorld _world;
        private readonly ActionEcsWorld _actionWorld;

        private EcsFilter<
            CollectItemToComponent
        > _itemsFilter;

        public CollectItemFlyIntoStackSystem(
            GameEcsWorld world,
            ActionEcsWorld actionWorld
        )
        {
            _world = world;
            _actionWorld = actionWorld;
        }

        public void Run()
        {
            foreach (var actionId in _itemsFilter)
            {
                var collectAction = _itemsFilter.Get1(actionId);

                var packedItem = collectAction.Item;
                if (!packedItem.TryUnpack(_world, out var itemEntity))
                    continue;

                var packedOwner = collectAction.TargetInventory;
                if (!packedOwner.TryUnpack(_world, out var ownerEntity))
                    continue;

                ref var stackHeight = ref ownerEntity.Get<StackInventoryHeightComponent>();
                var itemOffset = new Vector3(0, stackHeight.Value, 0);

                if (itemEntity.Has<HeightComponent>())
                {
                    var itemHeight = itemEntity.Get<HeightComponent>().Value;
                    stackHeight.Value += itemHeight;
                }

                if (ownerEntity.Has<StackInventoryParentComponent>())
                {
                    var target = ownerEntity.Get<StackInventoryParentComponent>().Value;

                    _actionWorld.FlyItemTo(packedItem, target, itemOffset);
                }
            }
        }
    }
}