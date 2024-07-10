using Ecs.Components;
using Ecs.Views;
using Leopotam.Ecs;

namespace Ecs.Systems.Initialize
{
    public class CameraInitializeSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world;
        private readonly CameraView _cameraView;

        public CameraInitializeSystem(
            EcsWorld world,
            CameraView cameraView
        )
        {
            _world = world;
            _cameraView = cameraView;
        }

        public void Init()
        {
            var cameraEntity = _world.NewEntity();

            cameraEntity.Get<CameraComponent>();

            cameraEntity.Get<ViewRefComponent<CameraView>>().Value = _cameraView;
        }
    }
}