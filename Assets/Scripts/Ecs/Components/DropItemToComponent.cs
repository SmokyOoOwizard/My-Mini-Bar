using Ecs.Utils;

namespace Ecs.Components
{
    public struct DropItemToComponent
    {
        public EntityId Slot;
        public EntityId Inventory;
    }
}