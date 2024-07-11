using Cinemachine;
using Ecs.Components;
using Ecs.Components.Camera;
using Ecs.Components.Refs;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Views.Impl
{
    public class CameraView : AEntityView
    {
        public CinemachineVirtualCamera virtualCamera;

        public void SetTarget(Transform target)
        {
            virtualCamera.Follow = target;
            virtualCamera.LookAt = target;
        }

        public override void Init(EcsEntity entity, EcsWorld world)
        {
            entity.Get<CameraComponent>();
            entity.Get<ActiveComponent>();
            entity.Get<TransformRefComponent>().Value = virtualCamera.transform;
            entity.Get<ViewRefComponent<CameraView>>().Value = this;
        }
    }
}