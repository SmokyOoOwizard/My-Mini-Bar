using DG.Tweening;
using Ecs.Components;
using Ecs.Core;
using Leopotam.Ecs;

namespace Ecs.Systems.Update
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