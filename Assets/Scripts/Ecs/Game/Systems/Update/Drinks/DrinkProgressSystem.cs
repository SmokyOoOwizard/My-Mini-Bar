using Ecs.Core;
using Ecs.Game.Components;
using Ecs.Game.Components.Items;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Game.Systems.Update.Drinks
{
    public class DrinkProgressSystem : IUpdateEcsSystem
    {
        private EcsFilter<
            DrinkerComponent,
            DrinkProgressComponent,
            ItemRefComponent
        > _drinkersFilter;

        public void Run()
        {
            foreach (var drinkerId in _drinkersFilter)
            {
                var drinkerEntity = _drinkersFilter.GetEntity(drinkerId);

                ref var drinkProgress = ref drinkerEntity.Get<DrinkProgressComponent>();
                drinkProgress.Value += Time.deltaTime;
            }
        }
    }
}