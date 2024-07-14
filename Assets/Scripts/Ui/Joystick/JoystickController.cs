using Core;
using Ecs.Components;
using Ecs.Components.Camera;
using Ecs.Components.Input;
using Ecs.Components.Refs;
using Ecs.Core;
using Ecs.Utils;
using Ecs.Worlds;
using Leopotam.Ecs;
using SimpleUi.Abstracts;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui.Joystick
{
    public class JoystickController : UiController<JoystickView>, IUiInitializable
    {
        private readonly EcsFilter<MoveDirectionComponent> _moveFilter;
        private readonly EcsFilter<ActiveComponent, CameraComponent, TransformRefComponent> _activeCameraFilter;

        private Vector2 _dragStart;

        public JoystickController(GameEcsWorld world)
        {
            _moveFilter = world.GetFilter<EcsFilter<MoveDirectionComponent>>();
            _activeCameraFilter = world.GetFilter<EcsFilter<ActiveComponent, CameraComponent, TransformRefComponent>>();
        }

        public void Initialize()
        {
            View.touchZone.OnPointerDownCmd.Subscribe(OnPointerDown).AddTo(View);
            View.touchZone.OnPointerUpCmd.Subscribe(OnPointerUp).AddTo(View);
            View.touchZone.OnPointerMoveCmd.Subscribe(OnPointerMove).AddTo(View);
        }

        public override void OnShow()
        {
            View.background.gameObject.SetActive(false);
            View.handle.gameObject.SetActive(false);
        }

        private void OnPointerDown(PointerEventData eventData)
        {
            View.background.gameObject.SetActive(true);
            View.handle.gameObject.SetActive(true);

            _dragStart = eventData.position;

            View.background.rectTransform.position = _dragStart;
            View.handle.rectTransform.position = _dragStart;
        }

        private void OnPointerUp(PointerEventData eventData)
        {
            View.background.gameObject.SetActive(false);
            View.handle.gameObject.SetActive(false);

            UpdateInput(Vector2.zero);
        }

        private void OnPointerMove(PointerEventData eventData)
        {
            var delta = eventData.position - _dragStart;
            var radius = View.radius;


            var magnitude = delta.magnitude;
            var dir = delta / magnitude;

            if (magnitude > radius)
                delta = dir * radius;

            View.handle.rectTransform.position = _dragStart + delta;


            UpdateInput(dir);
        }

        private void UpdateInput(Vector2 dir)
        {
            var yAngle = 0f;
            if (!_activeCameraFilter.IsEmpty())
            {
                var cameraTransform = _activeCameraFilter.Get3(0).Value;
                yAngle = cameraTransform.rotation.eulerAngles.y;
            }

            if (!_moveFilter.IsEmpty())
            {
                ref var moveComp = ref _moveFilter.Get1(0);

                var rotation = Quaternion.Euler(0, 0, yAngle + 90);

                var finalDir = rotation * dir;

                moveComp.Value = finalDir;
            }
        }
    }
}