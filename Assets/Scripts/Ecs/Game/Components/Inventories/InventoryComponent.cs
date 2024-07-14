using System.Collections.Generic;
using Ecs.Utils;
using Leopotam.Ecs;

namespace Ecs.Game.Components.Inventories
{
    public struct StackInventoryComponent : IEcsAutoReset<StackInventoryComponent>
    {
        public Stack<EntityId> Value;

        public void AutoReset(ref StackInventoryComponent c)
        {
            c.Value = new();
        }
    }
}