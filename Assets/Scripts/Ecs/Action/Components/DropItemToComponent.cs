using Ecs.Utils;

namespace Ecs.Action.Components
{
    public struct DropItemToComponent
    {
        public EntityId Slot;
        public EntityId Inventory;
    }
}