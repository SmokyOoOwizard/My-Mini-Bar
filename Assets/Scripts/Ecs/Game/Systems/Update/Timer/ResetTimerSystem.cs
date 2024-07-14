using Ecs.Core;
using Ecs.Game.Components;
using Ecs.Game.Components.Timer;
using Leopotam.Ecs;

namespace Ecs.Game.Systems.Update.Timer
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