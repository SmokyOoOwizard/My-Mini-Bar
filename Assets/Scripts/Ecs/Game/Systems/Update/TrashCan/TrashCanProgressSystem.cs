using Ecs.Core;
using Ecs.Game.Components;
using Ecs.Game.Components.Items;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Game.Systems.Update.TrashCan
{
    public class TrashCanProgressSystem : IUpdateEcsSystem
    {
        private EcsFilter<
            TrashCanComponent,
            TrashDeletingProgressComponent,
            ItemRefComponent
        > _drinkersFilter;

        public void Run()
        {
            foreach (var trashId in _drinkersFilter)
            {
                var trashEntity = _drinkersFilter.GetEntity(trashId);

                ref var deletingProgress = ref trashEntity.Get<TrashDeletingProgressComponent>();
                deletingProgress.Value += Time.deltaTime;
            }
        }
    }
}