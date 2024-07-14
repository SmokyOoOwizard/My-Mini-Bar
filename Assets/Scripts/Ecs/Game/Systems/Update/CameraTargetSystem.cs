using Ecs.Core;
using Ecs.Game.Components.Camera;
using Ecs.Game.Components.Refs;
using Ecs.Views.Impl;
using Leopotam.Ecs;

namespace Ecs.Game.Systems.Update
{
    public class CameraTargetSystem : IUpdateEcsSystem
    {
        private EcsFilter<CameraComponent, ViewRefComponent<CameraView>> _cameraFilter;
        private EcsFilter<TransformRefComponent, CameraTargetComponent> _targetFilter;

        public void Run()
        {
            foreach (var cameraId in _cameraFilter)
            {
                var cameraView = _cameraFilter.Get2(cameraId).Value;

                foreach (var entity in _targetFilter)
                {
                    var target = _targetFilter.Get1(entity).Value;

                    cameraView.SetTarget(target);
                }
            }
        }
    }
}