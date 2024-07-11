using System.Collections.Generic;
using Ecs.Utils;
using Leopotam.Ecs;

namespace Ecs.Components.Spawner
{
    public struct ItemSlotsComponent : IEcsAutoReset<ItemSlotsComponent>
    {
        public List<EntityId> Value;
        
        public void AutoReset(ref ItemSlotsComponent c)
        {
            c.Value = new();
        }
    }
}