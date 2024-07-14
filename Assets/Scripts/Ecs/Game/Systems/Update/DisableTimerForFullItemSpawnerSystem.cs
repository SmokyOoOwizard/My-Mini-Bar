using Ecs.Core;
using Ecs.Game.Components;
using Ecs.Game.Components.Spawner;
using Ecs.Game.Components.Timer;
using Leopotam.Ecs;

namespace Ecs.Game.Systems.Update
{
    public class DisableTimerForFullItemSpawnerSystem : IUpdateEcsSystem
    {
        private EcsFilter<
            ItemSpawnerComponent,
            TimerLeftComponent,
            TimerComponent,
            FullComponent
        > _spawnerFilter;

        public void Run()
        {
            foreach (var spawnerId in _spawnerFilter)
            {
                var timer = _spawnerFilter.Get3(spawnerId).Value;
                _spawnerFilter.Get2(spawnerId).Value = timer;
            }
        }
    }
}