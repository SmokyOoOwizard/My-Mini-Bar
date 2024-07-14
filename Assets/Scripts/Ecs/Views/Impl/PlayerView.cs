using Ecs.Game.Components;
using Ecs.Game.Components.Camera;
using Ecs.Game.Components.Inventories;
using Ecs.Game.Components.Parameters;
using Ecs.Game.Components.Refs;
using Ecs.Utils;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Views.Impl
{
    public class PlayerView : AEntityView
    {
        public float Speed;
        public float rotationSpeed;
        public float PickUpDistance;
        public int MaxStackSize;
        public Transform stackInventoryParent;
        public CharacterController characterController;

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

            entity.Get<RotationSpeedComponent>().Value = rotationSpeed;

            entity.Get<ViewRefComponent<CharacterController>>().Value = characterController;
        }
    }
}