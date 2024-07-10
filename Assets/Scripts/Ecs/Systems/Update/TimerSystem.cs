using Ecs.Components;
using Ecs.Components.Timer;
using Ecs.Core;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems.Update
{
    public class TimerSystem : IUpdateEcsSystem
    {
        private EcsFilter<TimerLeftComponent>.Exclude<DoneComponent> _timerFilter;
        
        public void Run()
        {
            foreach (var entityId in _timerFilter)
            {
                ref var timer = ref _timerFilter.Get1(entityId);
                
                timer.Value -= Time.deltaTime;
                
                if (timer.Value <= 0)
                {
                    var entity = _timerFilter.GetEntity(entityId);
                    entity.Get<DoneComponent>();
                    entity.Del<TimerLeftComponent>();
                }
            }
        }
    }
}