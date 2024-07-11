using Ecs.Components;
using Ecs.Components.Camera;
using Ecs.Components.Inventories;
using Ecs.Components.Parameters;
using Ecs.Components.Refs;
using Ecs.Utils;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Views.Impl
{
    public class PlayerView : AEntityView
    {
        public float Speed;
        public float PickUpDistance;
        public int MaxStackSize;
        public Transform stackInventoryParent;

        public override void Init(EcsEntity entity, EcsWorld world)
        {
            gameObject.Link(entity);

            entity.Get<PlayerComponent>();

            entity.Get<TransformRefComponent>().Value = transform;

            entity.Get<SpeedComponent>().Value = Speed;

            entity.Get<CameraTargetComponent>();

            entity.Get<ViewRefComponent<PlayerView>>().Value = this;

            entity.Get<StackInventoryComponent>();

            entity.Get<MaxItemsComponent>().Value = MaxStackSize;

            entity.Get<PickUpDistanceComponent>().Value = PickUpDistance;

            entity.Get<StackInventoryParentComponent>().Value = stackInventoryParent;
            entity.Get<StackInventoryHeightComponent>();
        }
    }
}