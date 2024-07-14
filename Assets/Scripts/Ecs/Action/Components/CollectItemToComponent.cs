using Ecs.Utils;

namespace Ecs.Action.Components
{
    public struct CollectItemToComponent
    {
        public EntityId Item;
        public EntityId TargetInventory;
    }
}