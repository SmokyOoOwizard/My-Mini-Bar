using Ecs.Game.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Views.Impl
{
    public class TrashCanView : ItemReceiverSlotView
    {
        [SerializeField]
        private float deleteDuration = 3f;

        public override void Init(EcsEntity entity, EcsWorld world)
        {
            base.Init(entity, world);

            entity.Get<TrashCanComponent>();
            entity.Get<TrashDeletingDurationComponent>().Value = deleteDuration;
        }
    }
}