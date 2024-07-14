using Ecs.Components;
using Ecs.Components.Inventories;
using Ecs.Components.Items;
using Ecs.Components.Refs;
using Ecs.Core;
using Ecs.Utils;
using Ecs.Worlds;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems.Update.Collectables
{
    public class DropItemSystem : IUpdateEcsSystem
    {
        private readonly GameEcsWorld _world;
        private readonly ActionEcsWorld _actionWorld;

        private EcsFilter<
            DropItemToComponent
        > _actionsFilter;

        public DropItemSystem(
            GameEcsWorld world,
            ActionEcsWorld actionWorld
        )
        {
            _world = world;
            _actionWorld = actionWorld;
        }

        public void Run()
        {
            foreach (var actionId in _actionsFilter)
            {
                var action = _actionsFilter.Get1(actionId);
                var packedInventory = action.Inventory;
                if (!packedInventory.TryUnpack(_world, out var inventoryEntity))
                    continue;

                var packedSlot = action.Slot;
                if (!packedSlot.TryUnpack(_world, out var slotEntity))
                    continue;

                var inventory = inventoryEntity.Get<StackInventoryComponent>().Value;
                if (inventory.Count == 0)
                    continue;

                if (slotEntity.Has<ItemRefComponent>())
                    continue;

                var packedItem = inventory.Pop();

                slotEntity.Get<ItemRefComponent>().Value = packedItem;

                inventoryEntity.Get<InventoryUpdatedComponent>();

                var stackParent = slotEntity.Get<TransformRefComponent>().Value;

                _actionWorld.FlyItemTo(packedItem, stackParent, Vector3.zero);
            }
        }
    }
}