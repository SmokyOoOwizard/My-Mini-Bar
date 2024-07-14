using Ecs.Core;
using Ecs.Game.Components.Inventories;
using Ecs.Game.Components.Refs;
using Ecs.Views.Impl;
using Leopotam.Ecs;

namespace Ecs.Game.Systems.Update
{
    public class InventoryViewNotifyerSystem : IUpdateEcsSystem
    {
        private EcsFilter<
            StackInventoryComponent,
            InventoryUpdatedComponent,
            ViewRefComponent<PlayerView>
        > _inventoryFilter;

        public void Run()
        {
            foreach (var inventoryId in _inventoryFilter)
            {
                var inventory = _inventoryFilter.Get1(inventoryId).Value;

                var view = _inventoryFilter.Get3(inventoryId);

                var carrying = inventory.Count > 0;
                view.Value.SetCarrying(carrying);
            }
        }
    }
}