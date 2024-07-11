using System.Collections.Generic;
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
        private EcsFilter<
            PlayerComponent,
            InventoryComponent<Stack<EntityId>>,
            StackInventoryParentComponent,
            StackInventoryHeightComponent
        > _playerFilter;

        private EcsFilter<
            CollectComponent,
            TransformRefComponent,
            HeightComponent
        > _itemsFilter;

        public void Run()
        {
            if (_playerFilter.IsEmpty())
                return;

            var inventory = _playerFilter.Get2(0).Value;
            var stackParent = _playerFilter.Get3(0).Value;
            ref var stackHeight = ref _playerFilter.Get4(0).Value;

            foreach (var itemId in _itemsFilter)
            {
                var itemEntity = _itemsFilter.GetEntity(itemId);

                var packedItem = itemEntity.Pack();

                inventory.Push(packedItem);

                itemEntity.Del<CollectComponent>();

                var itemTransform = _itemsFilter.Get2(itemId).Value;

                var itemOffset = new Vector3(0, stackHeight, 0);

                var tween = itemTransform.DOMoveInTargetLocalSpace(stackParent, itemOffset, 1);
                tween.onComplete = () => itemTransform.SetParent(stackParent);
                tween.SetAutoKill(true);

                var itemHeight = _itemsFilter.Get3(itemId).Value;
                stackHeight += itemHeight;
            }
        }
    }
}