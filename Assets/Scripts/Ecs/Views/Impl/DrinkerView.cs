using Ecs.Game.Components;
using Ecs.Game.Components.Drinker;
using Ecs.Game.Components.Refs;
using Leopotam.Ecs;
using UniRx;
using UnityEngine;
using Utils;

namespace Ecs.Views.Impl
{
    public class DrinkerView : ItemReceiverSlotView
    {
        [SerializeField]
        private float drinkDuration = 3f;

        [SerializeField]
        private Animator animator;

        [SerializeField]
        private Transform itemHandParent;

        [SerializeField]
        private DrinkerAnimationCallbacks animationCallbacks;

        private EcsEntity _entity;

        public override void Init(EcsEntity entity, EcsWorld world)
        {
            base.Init(entity, world);

            _entity = entity;

            entity.Get<DrinkerComponent>();
            entity.Get<DrinkDurationComponent>().Value = drinkDuration;

            entity.Get<ViewRefComponent<DrinkerView>>().Value = this;

            animationCallbacks.PickedUpCmd.Subscribe(_ => OnPickedUp()).AddTo(this);
            animationCallbacks.PickUpCmd.Subscribe(_ => OnPickUp()).AddTo(this);
            animationCallbacks.DrinkedCmd.Subscribe(_ => OnDrinked()).AddTo(this);
        }


        public void SetPickUp()
        {
            animator.SetTrigger(AnimationKeys.PickUp);
        }

        public void SetDrink()
        {
            animator.SetTrigger(AnimationKeys.Drink);
        }

        private void OnPickUp()
        {
            var item = slotTransform.GetChild(0);
            item.SetParent(itemHandParent);
            item.localPosition = Vector3.zero;
        }

        private void OnPickedUp()
        {
            _entity.Get<PickedUpComponent>();
        }

        private void OnDrinked()
        {
            _entity.Get<DrinkedComponent>();
        }
    }
}