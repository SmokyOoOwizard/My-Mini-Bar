using Ecs.Game.Components;
using Ecs.Game.Components.Items;
using Ecs.Game.Components.Parameters;
using Ecs.Game.Components.Refs;
using Ecs.Utils;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Views.Impl
{
    public class ItemReceiverSlotView : AEntityView
    {
        public float pickUpDistance = 0.5f;
        public EItemFilter filter = EItemFilter.Any;
        public Transform slotTransform;

        public override void Init(EcsEntity entity, EcsWorld world)
        {
            entity.Get<TransformRefComponent>().Value = slotTransform;
            entity.Get<ItemSlotComponent>();
            entity.Get<PickUpDistanceComponent>();
            entity.Get<ReceiverComponent>();
            entity.Get<PickUpDistanceComponent>().Value = pickUpDistance;
            entity.Get<ItemFilterComponent>().Value = filter;
        }
    }
}