using System;
using Leopotam.Ecs;
using Zenject;

namespace Ecs.Core
{
    public class EcsBootstrap : ITickable, IDisposable
    {
        private readonly EcsWorld _world;
        private readonly EcsSystems _systems;
    
        public EcsBootstrap(
            IEcsSystem[] systems
        )
        {
            _world = new EcsWorld();

            _systems = new EcsSystems(_world);
            foreach (var system in systems)
            {
                _systems.Add(system);
            }
        
            _systems.Init();
        }

        public void Tick()
        {
            _systems.Run();
        }

        public void Dispose()
        {
            _systems.Destroy();
            _world.Destroy();
        }
    }
}