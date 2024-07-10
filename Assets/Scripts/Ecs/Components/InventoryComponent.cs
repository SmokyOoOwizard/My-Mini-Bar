using System.Collections.Generic;
using Ecs.Utils;
using Leopotam.Ecs;

namespace Ecs.Components
{
    public struct InventoryComponent<T> : IEcsAutoReset<InventoryComponent<T>> where T : IEnumerable<EntityId>, new()
    {
        public T Value;

        public void AutoReset(ref InventoryComponent<T> c)
        {
            c.Value = new();
        }
    }
}