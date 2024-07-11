using Ecs.Components;
using Ecs.Components.Items;
using Ecs.Components.Parameters;
using Ecs.Components.Refs;
using Ecs.Utils;
using Leopotam.Ecs;

namespace Ecs.Views.Impl
{
    public class ItemReceiverSlotView : AEntityView
    {
        public float pickUpDistance = 0.5f;
        public EItemFilter filter = EItemFilter.Any;

        public override void Init(EcsEntity entity, EcsWorld world)
        {
            entity.Get<TransformRefComponent>().Value = transform;
            entity.Get<ItemSlotComponent>();
            entity.Get<PickUpDistanceComponent>();
            entity.Get<ReceiverComponent>();
            entity.Get<PickUpDistanceComponent>().Value = pickUpDistance;
            entity.Get<ItemFilterComponent>().Value = filter;
        }
    }
}