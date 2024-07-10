using Ecs.Components;
using Ecs.Components.Timer;
using Ecs.Core;
using Leopotam.Ecs;

namespace Ecs.Systems.Update
{
    public class ResetTimerSystem : IUpdateEcsSystem
    {
        private EcsFilter<TimerComponent, ResetTimerComponent> _timerFilter;

        public void Run()
        {
            foreach (var entityId in _timerFilter)
            {
                var entity = _timerFilter.GetEntity(entityId);
                if (entity.Has<DoneComponent>())
                    entity.Del<DoneComponent>();

                var timer = entity.Get<TimerComponent>().Value;
                entity.Get<TimerLeftComponent>().Value = timer;
                
                entity.Del<ResetTimerComponent>();
            }
        }
    }
}