using Cinemachine;
using Ecs.Game.Components;
using Ecs.Game.Components.Camera;
using Ecs.Game.Components.Refs;
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