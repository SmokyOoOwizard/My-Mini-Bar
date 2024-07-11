using DG.Tweening;
using Ecs.Components;
using Ecs.Components.Inventories;
using Ecs.Components.Items;
using Ecs.Components.Refs;
using Ecs.Core;
using Ecs.Utils;
using Leopotam.Ecs;
using UnityEngine;
using Utils.Dotween;

namespace Ecs.Systems.Update.Collectables
{
    public class DropItemSystem : IUpdateEcsSystem
    {
        private readonly EcsWorld _world;

        private EcsFilter<
            DropItemToComponent,
            StackInventoryComponent
        > _inventoriesFilter;

        public DropItemSystem(EcsWorld world)
        {
            _world = world;
        }

        public void Run()
        {
            foreach (var inventoryId in _inventoriesFilter)
            {
                var entity = _inventoriesFilter.GetEntity(inventoryId);

                var inventory = _inventoriesFilter.Get2(inventoryId).Value;
                if (inventory.Count == 0)
                    continue;


                var packedReceiver = _inventoriesFilter.Get1(inventoryId).Value;
                if (!packedReceiver.TryUnpack(_world, out var receiverEntity))
                    continue;

                if (receiverEntity.Has<ItemRefComponent>())
                    continue;

                var packedItem = inventory.Pop();

                receiverEntity.Get<ItemRefComponent>().Value = packedItem;

                entity.Get<InventoryUpdatedComponent>();

                if (!packedItem.TryUnpack(_world, out var itemEntity))
                    continue;

                var itemTransform = itemEntity.Get<TransformRefComponent>().Value;
                var stackParent = receiverEntity.Get<TransformRefComponent>().Value;
                var tween = itemTransform.DOMoveInTargetLocalSpace(stackParent, Vector3.zero, 1);
                tween.onComplete = () => itemTransform.SetParent(stackParent);
                tween.SetAutoKill(true);

                entity.Del<DropItemToComponent>();
            }
        }
    }
}