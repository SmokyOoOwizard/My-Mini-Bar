using System;
using System.Collections.Generic;
using Ecs.Game;
using Ecs.Game.Components;
using Ecs.Game.Components.Refs;
using Ecs.Utils;
using Ecs.Worlds;
using Leopotam.Ecs;
using SimpleUi.Abstracts;
using UnityEngine;
using Zenject;

namespace Ui.TrashCanToolTip
{
    public class TrashCanToolTipsController : UiController<TrashCanToolTipsView>,
                                              IEcsFilterListener,
                                              ILateTickable,
                                              IDisposable
    {
        private readonly Dictionary<EcsEntity, TrashCanToolTipView> _activeTips = new();

        private readonly EcsFilter<
            TrashCanComponent,
            ViewInitedComponent
        >.Exclude<TrashDeletingProgressComponent> _filter;

        public TrashCanToolTipsController(
            GameEcsWorld world
        )
        {
            _filter = world.GetFilter<EcsFilter<
                TrashCanComponent,
                ViewInitedComponent
            >.Exclude<TrashDeletingProgressComponent>>();

            _filter.AddListener(this);
        }


        public void OnEntityAdded(in EcsEntity entity)
        {
            var tip = View.toolTipCollection.Create();

            _activeTips[entity] = tip;
        }

        public void OnEntityRemoved(in EcsEntity entity)
        {
            if (!_activeTips.ContainsKey(entity))
                return;

            var tip = _activeTips[entity];

            View.toolTipCollection.Despawn(tip);
            _activeTips.Remove(entity);
        }

        public void LateTick()
        {
            var camera = Camera.main;
            if (camera == null)
                return;

            foreach (var (entity, tip) in _activeTips)
            {
                var transform = entity.Get<TransformRefComponent>().Value;

                var viewPosition = camera.WorldToViewportPoint(transform.position);

                var rectTransform = (RectTransform)tip.transform;

                var targetPos = new Vector2(viewPosition.x * Screen.width, viewPosition.y * Screen.height);

                rectTransform.anchoredPosition = targetPos;
            }
        }

        public void Dispose()
        {
            _filter.RemoveListener(this);
        }
    }
}