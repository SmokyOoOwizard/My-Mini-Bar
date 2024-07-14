using Ecs.Core;
using Ecs.Game.Components;
using Ecs.Game.Components.Items;
using Ecs.Utils;
using Ecs.Worlds;
using Leopotam.Ecs;

namespace Ecs.Game.Systems.Update.TrashCan
{
    public class TrashCanFinishSystem : IUpdateEcsSystem
    {
        private readonly GameEcsWorld _world;

        private EcsFilter<
            TrashCanComponent,
            TrashDeletingProgressComponent,
            TrashDeletingDurationComponent,
            ItemRefComponent
        > _drinkersFilter;

        public TrashCanFinishSystem(GameEcsWorld world)
        {
            _world = world;
        }

        public void Run()
        {
            foreach (var trashId in _drinkersFilter)
            {
                var trashEntity = _drinkersFilter.GetEntity(trashId);

                var deletingProgress = _drinkersFilter.Get2(trashId).Value;
                var deletingDuration = _drinkersFilter.Get3(trashId).Value;

                if (deletingProgress < deletingDuration)
                    continue;

                var packedItem = _drinkersFilter.Get4(trashId).Value;

                if (!packedItem.TryUnpack(_world, out var itemEntity))
                    continue;
                
                trashEntity.Del<TrashDeletingProgressComponent>();
                trashEntity.Del<ItemRefComponent>();

                itemEntity.Get<DestroyedComponent>();
            }
        }
    }
}