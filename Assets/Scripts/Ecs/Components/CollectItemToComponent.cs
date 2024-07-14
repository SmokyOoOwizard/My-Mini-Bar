using Ecs.Utils;

namespace Ecs.Components
{
    public struct CollectItemToComponent
    {
        public EntityId Item;
        public EntityId TargetInventory;
    }
}