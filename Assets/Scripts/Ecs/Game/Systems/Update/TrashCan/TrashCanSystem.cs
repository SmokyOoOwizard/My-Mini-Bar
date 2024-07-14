using Ecs.Core;
using Ecs.Game.Components;
using Ecs.Game.Components.Items;
using Ecs.Utils;
using Ecs.Worlds;
using Leopotam.Ecs;

namespace Ecs.Game.Systems.Update.TrashCan
{
    public class TrashCanSystem : IUpdateEcsSystem
    {
        private readonly GameEcsWorld _world;

        private EcsFilter<
            TrashCanComponent,
            ItemRefComponent
        >.Exclude<TrashDeletingProgressComponent> _drinkersFilter;

        public TrashCanSystem(GameEcsWorld world)
        {
            _world = world;
        }

        public void Run()
        {
            foreach (var trashId in _drinkersFilter)
            {
                var trashEntity = _drinkersFilter.GetEntity(trashId);
                var packedItem = _drinkersFilter.Get2(trashId).Value;
                if (!packedItem.TryUnpack(_world, out var itemEntity))
                    continue;

                if (itemEntity.Has<TweenComponent>())
                    continue;

                trashEntity.Get<TrashDeletingProgressComponent>();
            }
        }
    }
}