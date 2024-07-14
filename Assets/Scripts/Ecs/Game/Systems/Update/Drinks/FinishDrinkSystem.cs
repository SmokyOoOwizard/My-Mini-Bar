using System;
using Ecs.Core;
using Ecs.Game.Components;
using Ecs.Game.Components.Items;
using Ecs.Utils;
using Ecs.Worlds;
using Leopotam.Ecs;
using Random = UnityEngine.Random;

namespace Ecs.Game.Systems.Update.Drinks
{
    public class FinishDrinkSystem : IUpdateEcsSystem
    {
        private readonly GameEcsWorld _world;

        private EcsFilter<
            DrinkerComponent,
            DrinkProgressComponent,
            DrinkDurationComponent,
            ItemRefComponent
        > _drinkersFilter;

        public FinishDrinkSystem(GameEcsWorld world)
        {
            _world = world;
        }

        public void Run()
        {
            foreach (var drinkerId in _drinkersFilter)
            {
                var drinkerEntity = _drinkersFilter.GetEntity(drinkerId);

                var drinkProgress = drinkerEntity.Get<DrinkProgressComponent>();
                var drinkDuration = drinkerEntity.Get<DrinkDurationComponent>();

                if (drinkProgress.Value < drinkDuration.Value)
                    continue;

                var packedItem = _drinkersFilter.Get4(drinkerId).Value;

                if (!packedItem.TryUnpack(_world, out var itemEntity))
                    continue;

                drinkerEntity.Del<DrinkProgressComponent>();
                drinkerEntity.Del<ItemRefComponent>();

                itemEntity.Get<DestroyedComponent>();

                drinkerEntity.Get<ItemFilterComponent>().Value = GetRandomItemType();
            }
        }

        private EItemFilter GetRandomItemType()
        {
            var values = Enum.GetValues(typeof(EItemFilter));
            return (EItemFilter)values.GetValue(Random.Range(0, values.Length));
        }
    }
}