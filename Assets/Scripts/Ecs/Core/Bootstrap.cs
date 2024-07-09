using System;
using Leopotam.Ecs;
using Zenject;

namespace Ecs.Core
{
    public class EcsBootstrap : ITickable, IFixedTickable, IDisposable
    {
        private readonly EcsWorld _world;
        private readonly EcsSystems _updateSystems;
        private readonly EcsSystems _fixedSystems;

        public EcsBootstrap(
            EcsWorld world,
            IEcsInitSystem[] initSystems,
            IUpdateEcsSystem[] updateSystems,
            IFixedUpdateEcsSystem[] fixedSystems
        )
        {
            _world = world;
            _updateSystems = new EcsSystems(_world, "Update");
            foreach (var initSystem in initSystems)
            {
                _updateSystems.Add(initSystem);
            }

            foreach (var system in updateSystems)
            {
                _updateSystems.Add(system);
            }


            _fixedSystems = new EcsSystems(_world, "Fixed");
            foreach (var system in fixedSystems)
            {
                _fixedSystems.Add(system);
            }

#if UNITY_EDITOR
            Leopotam.Ecs.UnityIntegration.EcsWorldObserver.Create(_world);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_updateSystems);
            Leopotam.Ecs.UnityIntegration.EcsSystemsObserver.Create(_fixedSystems);
#endif

            _updateSystems.Init();
            _fixedSystems.Init();


        }

        public void Tick()
        {
            _updateSystems.Run();
        }

        public void FixedTick()
        {
            _fixedSystems.Run();
        }

        public void Dispose()
        {
            _updateSystems.Destroy();
            _fixedSystems.Destroy();
            _world.Destroy();
        }
    }
}