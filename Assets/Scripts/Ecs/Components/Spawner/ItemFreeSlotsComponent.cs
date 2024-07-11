using System.Collections.Generic;
using Ecs.Utils;
using Leopotam.Ecs;

namespace Ecs.Components.Spawner
{
    public struct ItemFreeSlotsComponent : IEcsAutoReset<ItemFreeSlotsComponent>
    {
        public List<EntityId> Value;
        
        public void AutoReset(ref ItemFreeSlotsComponent c)
        {
            c.Value = new();
        }
    }
}