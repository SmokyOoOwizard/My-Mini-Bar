using Ecs.Components;
using Ecs.Components.Refs;
using Ecs.Core;
using Ecs.Views;
using Leopotam.Ecs;

namespace Ecs.Systems.Update
{
    public class CameraTargetSystem : IUpdateEcsSystem
    {
        private EcsFilter<CameraComponent, ViewRefComponent<CameraView>> _cameraFilter;
        private EcsFilter<TransformRefComponent, CameraTargetComponent> _targetFilter;
        
        public void Run()
        {
            if (_cameraFilter.IsEmpty())
                return;

            var cameraView = _cameraFilter.Get2(0).Value;

            foreach (var entity in _targetFilter)
            {
                var target = _targetFilter.Get1(entity).Value;
                
                cameraView.SetTarget(target);
            }
        }
    }
}