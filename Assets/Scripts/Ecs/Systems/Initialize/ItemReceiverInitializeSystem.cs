using Ecs.Components;
using Ecs.Components.Items;
using Ecs.Components.Parameters;
using Ecs.Components.Refs;
using Ecs.Views;
using Leopotam.Ecs;

namespace Ecs.Systems.Initialize
{
    public class ItemReceiverInitializeSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world;
        private readonly ItemReceiverSlotView[] _itemReceiverViews;

        public ItemReceiverInitializeSystem(
            EcsWorld world,
            ItemReceiverSlotView[] itemReceiverViews
        )
        {
            _world = world;
            _itemReceiverViews = itemReceiverViews;
        }

        public void Init()
        {
            foreach (var receiverView in _itemReceiverViews)
            {
                var slotEntity = _world.NewEntity();
                slotEntity.Get<TransformRefComponent>().Value = receiverView.transform;
                slotEntity.Get<ItemSlotComponent>();
                slotEntity.Get<PickUpDistanceComponent>();
                slotEntity.Get<ReceiverComponent>();
                slotEntity.Get<PickUpDistanceComponent>().Value = receiverView.pickUpDistance;
                slotEntity.Get<ItemFilterComponent>().Value = receiverView.filter;
            }
        }
    }
}