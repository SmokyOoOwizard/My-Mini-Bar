using Ecs.Core;
using Ecs.Game.Components;
using Ecs.Game.Components.Drinker;
using Ecs.Game.Components.Items;
using Ecs.Game.Components.Refs;
using Ecs.Utils;
using Ecs.Views.Impl;
using Ecs.Worlds;
using Leopotam.Ecs;

namespace Ecs.Game.Systems.Update.Drinks
{
    public class DrinkSystem : IUpdateEcsSystem
    {
        private readonly GameEcsWorld _world;

        private EcsFilter<
            DrinkerComponent,
            ItemRefComponent,
            ViewRefComponent<DrinkerView>
        >.Exclude<DrinkProgressComponent> _drinkersFilter;

        public DrinkSystem(GameEcsWorld world)
        {
            _world = world;
        }

        public void Run()
        {
            foreach (var drinkerId in _drinkersFilter)
            {
                var drinkerEntity = _drinkersFilter.GetEntity(drinkerId);
                var packedItem = _drinkersFilter.Get2(drinkerId).Value;
                if (!packedItem.TryUnpack(_world, out var itemEntity))
                    continue;

                if (itemEntity.Has<TweenComponent>())
                    continue;

                drinkerEntity.Get<DrinkProgressComponent>();

                var view = _drinkersFilter.Get3(drinkerId).Value;
                
                view.SetPickUp();
                view.SetDrink();
            }
        }
    }
}