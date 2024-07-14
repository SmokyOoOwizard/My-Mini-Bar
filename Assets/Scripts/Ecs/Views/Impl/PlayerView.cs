using Ecs.Game.Components;
using Ecs.Game.Components.Camera;
using Ecs.Game.Components.Inventories;
using Ecs.Game.Components.Parameters;
using Ecs.Game.Components.Refs;
using Ecs.Utils;
using Leopotam.Ecs;
using UnityEngine;
using Utils;

namespace Ecs.Views.Impl
{
    public class PlayerView : AEntityView
    {
        [SerializeField]
        private float speed;

        [SerializeField]
        private float rotationSpeed;

        [SerializeField]
        private float pickUpDistance;

        [SerializeField]
        private int maxStackSize;

        [SerializeField]
        private Transform stackInventoryParent;

        [SerializeField]
        private CharacterController characterController;

        [SerializeField]
        private Animator animator;


        public override void Init(EcsEntity entity, EcsWorld world)
        {
            gameObject.Link(entity);

            entity.Get<PlayerComponent>();

            entity.Get<TransformRefComponent>().Value = transform;

            entity.Get<SpeedComponent>().Value = speed;

            entity.Get<CameraTargetComponent>();

            entity.Get<ViewRefComponent<PlayerView>>().Value = this;

            entity.Get<StackInventoryComponent>();

            entity.Get<MaxItemsComponent>().Value = maxStackSize;

            entity.Get<PickUpDistanceComponent>().Value = pickUpDistance;

            entity.Get<StackInventoryParentComponent>().Value = stackInventoryParent;
            entity.Get<StackInventoryHeightComponent>();

            entity.Get<RotationSpeedComponent>().Value = rotationSpeed;

            entity.Get<ViewRefComponent<CharacterController>>().Value = characterController;
        }

        public void SetWalking(bool isWalk)
        {
            animator.SetBool(AnimationKeys.Walk, isWalk);
        }
        
        public void SetCarrying(bool isCarrying)
        {
            animator.SetBool(AnimationKeys.Carrying, isCarrying);
        }
    }
}