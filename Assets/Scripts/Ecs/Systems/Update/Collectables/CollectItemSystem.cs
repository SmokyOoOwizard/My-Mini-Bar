using DG.Tweening;
using Ecs.Components;
using Ecs.Components.Parameters;
using Ecs.Components.Refs;
using Ecs.Core;
using Ecs.Utils;
using Leopotam.Ecs;
using UnityEngine;
using Utils.Dotween;

namespace Ecs.Systems.Update.Collectables
{
    public class CollectItemSystem : IUpdateEcsSystem
    {
        private readonly EcsWorld _world;

        private EcsFilter<
            CollectItemToComponent,
            TransformRefComponent,
            HeightComponent
        > _itemsFilter;

        public CollectItemSystem(EcsWorld world)
        {
            _world = world;
        }

        public void Run()
        {
            foreach (var itemId in _itemsFilter)
            {
                var itemEntity = _itemsFilter.GetEntity(itemId);
                var packedOwner = _itemsFilter.Get1(itemId).Value;
                if (!packedOwner.TryUnpack(_world, out var ownerEntity))
                    continue;

                itemEntity.Del<CollectItemToComponent>();

                var inventory = ownerEntity.Get<StackInventoryComponent>().Value;
                var packedItem = itemEntity.Pack();
                inventory.Push(packedItem);


                ref var stackHeight = ref ownerEntity.Get<StackInventoryHeightComponent>();
                var itemOffset = new Vector3(0, stackHeight.Value, 0);

                var itemTransform = _itemsFilter.Get2(itemId).Value;
                var stackParent = ownerEntity.Get<StackInventoryParentComponent>().Value;
                
                var tween = itemTransform.DOMoveInTargetLocalSpace(stackParent, itemOffset, 1);
                tween.onComplete = () => itemTransform.SetParent(stackParent);
                tween.SetAutoKill(true);

                var itemHeight = _itemsFilter.Get3(itemId).Value;
                stackHeight.Value += itemHeight;
            }
        }
    }
}