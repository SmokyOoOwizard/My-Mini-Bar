using Ecs.Components;
using Ecs.Components.Items;
using Ecs.Core;
using Ecs.Utils;
using Ecs.Worlds;
using Leopotam.Ecs;

namespace Ecs.Systems.Update.Drinks
{
    public class DrinkSystem : IUpdateEcsSystem
    {
        private readonly GameEcsWorld _world;

        private EcsFilter<
            DrinkerComponent,
            ItemRefComponent
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
            }
        }
    }
}