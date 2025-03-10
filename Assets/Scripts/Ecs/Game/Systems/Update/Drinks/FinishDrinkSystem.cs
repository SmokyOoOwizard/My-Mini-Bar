﻿using System;
using Ecs.Core;
using Ecs.Game.Components;
using Ecs.Game.Components.Drinker;
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
            ItemRefComponent,
            DrinkedComponent
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

                var drinkProgress = _drinkersFilter.Get2(drinkerId).Value;
                var drinkDuration = _drinkersFilter.Get3(drinkerId).Value;

                if (drinkProgress < drinkDuration)
                    continue;

                var packedItem = _drinkersFilter.Get4(drinkerId).Value;

                if (!packedItem.TryUnpack(_world, out var itemEntity))
                    continue;
                
                drinkerEntity.Get<ItemFilterComponent>().Value = GetRandomItemType();
                
                drinkerEntity.Del<DrinkProgressComponent>();
                drinkerEntity.Del<ItemRefComponent>();

                itemEntity.Get<DestroyedComponent>();
            }
        }

        private EItemFilter GetRandomItemType()
        {
            var values = Enum.GetValues(typeof(EItemFilter));
            return (EItemFilter)values.GetValue(Random.Range(0, values.Length));
        }
    }
}