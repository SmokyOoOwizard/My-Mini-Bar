﻿using DG.Tweening;
using Ecs.Components;
using Ecs.Components.Inventories;
using Ecs.Components.Parameters;
using Ecs.Components.Refs;
using Ecs.Core;
using Ecs.Utils;
using Ecs.Worlds;
using Leopotam.Ecs;
using UnityEngine;
using Utils.Dotween;

namespace Ecs.Systems.Update.Collectables
{
    public class CollectItemSystem : IUpdateEcsSystem
    {
        private readonly GameEcsWorld _world;

        private EcsFilter<
            CollectItemToComponent,
            TransformRefComponent,
            HeightComponent
        > _itemsFilter;

        public CollectItemSystem(GameEcsWorld world)
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
                
                itemEntity.Get<TweenComponent>().Value = tween;

                var itemHeight = _itemsFilter.Get3(itemId).Value;
                stackHeight.Value += itemHeight;
            }
        }
    }
}