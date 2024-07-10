using System.Collections.Generic;
using DG.Tweening;
using Ecs.Components;
using Ecs.Components.Collectables;
using Ecs.Components.Refs;
using Ecs.Core;
using Ecs.Utils;
using Leopotam.Ecs;
using UnityEngine;
using Utils.Dotween;

namespace Ecs.Systems.Update.Collectables
{
    public class CollectCollectableSystem : IUpdateEcsSystem
    {
        private EcsFilter<PlayerComponent, InventoryComponent<Stack<EntityId>>, StackInventoryParentComponent>
            _playerFilter;

        private EcsFilter<CollectableComponent, CollectComponent, TransformRefComponent> _itemsFilter;

        public void Run()
        {
            if (_playerFilter.IsEmpty())
                return;

            var inventory = _playerFilter.Get2(0).Value;
            var stackParent = _playerFilter.Get3(0).Value;

            foreach (var itemId in _itemsFilter)
            {
                var itemEntity = _itemsFilter.GetEntity(itemId);

                var packedItem = itemEntity.Pack();

                inventory.Push(packedItem);

                itemEntity.Del<CollectComponent>();
                itemEntity.Get<CollectedComponent>();

                var itemTransform = _itemsFilter.Get3(itemId).Value;

                var tween = itemTransform.DOMoveInTargetLocalSpace(stackParent, Vector3.zero, 1);
                tween.onComplete = () => itemTransform.SetParent(stackParent);
                tween.SetAutoKill(true);
            }
        }
    }
}