using DG.Tweening;
using Ecs.Core;
using Ecs.Game.Components;
using Leopotam.Ecs;

namespace Ecs.Game.Systems.Update
{
    public class TweenSystem : IUpdateEcsSystem
    {
        private EcsFilter<TweenComponent> _tweenFilter;

        public void Run()
        {
            foreach (var entityId in _tweenFilter)
            {
                var tween = _tweenFilter.Get1(entityId).Value;

                if (!tween.IsActive())
                {
                    var entity = _tweenFilter.GetEntity(entityId);
                    entity.Del<TweenComponent>();
                }
            }
        }
    }
}