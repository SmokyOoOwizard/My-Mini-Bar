using DG.Tweening;
using Ecs.Components;
using Ecs.Components.Refs;
using Ecs.Core;
using Ecs.Utils;
using Ecs.Worlds;
using Leopotam.Ecs;
using Utils.Dotween;

namespace Ecs.Systems.Update.Collectables
{
    public class FlyItemToSystem : IUpdateEcsSystem
    {
        private readonly GameEcsWorld _world;

        private EcsFilter<
            FlyItemToComponent
        > _itemsFilter;

        public FlyItemToSystem(GameEcsWorld world)
        {
            _world = world;
        }

        public void Run()
        {
            foreach (var actionId in _itemsFilter)
            {
                var action = _itemsFilter.Get1(actionId);

                var packedItem = action.Item;
                if (!packedItem.TryUnpack(_world, out var itemEntity))
                    continue;

                if (!itemEntity.Has<TransformRefComponent>())
                    continue;

                var itemTransform = itemEntity.Get<TransformRefComponent>().Value;
                var stackParent = action.Target;

                var tween = itemTransform.DOMoveInTargetLocalSpace(stackParent, action.Offset, 1);
                tween.onComplete = () => itemTransform.SetParent(stackParent);
                tween.SetAutoKill(true);

                itemEntity.Get<TweenComponent>().Value = tween;
            }
        }
    }
}