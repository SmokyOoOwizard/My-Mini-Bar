using Core;
using Ecs.Components.Input;
using Leopotam.Ecs;
using SimpleUi.Abstracts;
using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui.Joystick
{
    public class JoystickController : UiController<JoystickView>, IUiInitializable
    {
        private readonly EcsFilter<MoveDirectionComponent> _filter;

        private Vector2 _dragStart;

        public JoystickController(EcsWorld world)
        {
            _filter = (EcsFilter<MoveDirectionComponent>)world.GetFilter(typeof(EcsFilter<MoveDirectionComponent>));
        }

        public void Initialize()
        {
            View.touchZone.OnPointerDownCmd.Subscribe(OnPointerDown).AddTo(View);
            View.touchZone.OnPointerUpCmd.Subscribe(OnPointerUp).AddTo(View);
            View.touchZone.OnPointerMoveCmd.Subscribe(OnPointerMove).AddTo(View);
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
            if (!_filter.IsEmpty())
            {
                ref var moveComp = ref _filter.Get1(0);
                moveComp.Value = dir;
            }
        }
    }
}