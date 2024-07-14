using Ecs.Action.Components;
using Ecs.Game.Components;
using Ecs.Worlds;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Utils
{
    public static class ActionEcsWorldExtensions
    {
        public static void CollectItemTo(
            this ActionEcsWorld world,
            EntityId item,
            EntityId targetInventory
        )
        {
            var entity = world.NewEntity();

            entity.Get<ActionComponent>();

            ref var collectItemTo = ref entity.Get<CollectItemToComponent>();

            collectItemTo.Item = item;
            collectItemTo.TargetInventory = targetInventory;
        }

        public static void DropItemTo(
            this ActionEcsWorld world,
            EntityId inventory,
            EntityId slot
        )
        {
            var entity = world.NewEntity();

            entity.Get<ActionComponent>();

            ref var collectItemTo = ref entity.Get<DropItemToComponent>();

            collectItemTo.Inventory = inventory;
            collectItemTo.Slot = slot;
        }

        public static void FlyItemTo(
            this ActionEcsWorld world,
            EntityId item,
            Transform target,
            Vector3 offset
        )
        {
            var entity = world.NewEntity();

            entity.Get<ActionComponent>();

            ref var collectItemTo = ref entity.Get<FlyItemToComponent>();

            collectItemTo.Item = item;
            collectItemTo.Target = target;
            collectItemTo.Offset = offset;
        }
    }
}