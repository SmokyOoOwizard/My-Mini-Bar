using System;
using System.Collections.Generic;
using System.Linq;
using Leopotam.Ecs;
using Zenject;

namespace Ecs.Core
{
    public class EcsBootstrap : ITickable, IFixedTickable, IDisposable
    {
        private readonly List<EcsWorld> _worlds = new();
        private readonly List<EcsSystems> _updateSystems = new();
        private readonly List<EcsSystems> _fixedSystems = new();

        public EcsBootstrap(
            SystemInstaller[] rawSystems
        )
        {
            var systems = rawSystems
                         .GroupBy(c => c.World)
                         .ToDictionary(c => c.Key, c => c.Select(c => c.System).ToList());

            foreach (var (world, worldSystems) in systems)
            {
                _worlds.Add(world);
                var updateSystems = new EcsSystems(world, "Update");
                var fixedSystems = new EcsSystems(world, "Fixed");

                foreach (var system in worldSystems)
                {
                    if (system is IFixedUpdateEcsSystem)
                        fixedSystems.Add(system);
                    else
                        updateSystems.Add(system);
                }

#if UNITY_EDITOR
                Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(world);
                Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(updateSystems);
                Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(fixedSystems);
#endif

                updateSystems.Init();
                fixedSystems.Init();

                _updateSystems.Add(updateSystems);
                _fixedSystems.Add(fixedSystems);
            }
        }

        public void Tick()
        {
            foreach (var updateSystem in _updateSystems)
            {
                updateSystem.Run();
            }
        }

        public void FixedTick()
        {
            foreach (var fixedSystem in _fixedSystems)
            {
                fixedSystem.Run();
            }
        }

        public void Dispose()
        {
            foreach (var updateSystem in _updateSystems)
            {
                updateSystem.Destroy();
            }

            foreach (var fixedSystem in _fixedSystems)
            {
                fixedSystem.Destroy();
            }

            foreach (var world in _worlds)
            {
                world.Destroy();
            }
        }
    }
}