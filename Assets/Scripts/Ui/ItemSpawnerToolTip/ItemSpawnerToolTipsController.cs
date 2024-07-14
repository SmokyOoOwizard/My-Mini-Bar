using System;
using System.Collections.Generic;
using Ecs.Game;
using Ecs.Game.Components.Items;
using Ecs.Game.Components.Refs;
using Ecs.Game.Components.Spawner;
using Ecs.Utils;
using Ecs.Worlds;
using Leopotam.Ecs;
using SimpleUi.Abstracts;
using UnityEngine;
using Zenject;

namespace Ui.ItemSpawnerToolTip
{
    public class ItemSpawnerToolTipsController : UiController<ItemSpawnerToolTipsView>,
                                                 IEcsFilterListener,
                                                 ILateTickable,
                                                 IDisposable
    {
        private readonly Dictionary<EcsEntity, ItemSpawnerToolTipView> _activeTips = new();

        private readonly EcsFilter<
            ItemSpawnerComponent,
            ItemTypeComponent,
            ViewInitedComponent
        > _filter;

        public ItemSpawnerToolTipsController(
            GameEcsWorld world
        )
        {
            _filter = world.GetFilter<EcsFilter<
                ItemSpawnerComponent,
                ItemTypeComponent,
                ViewInitedComponent
            >>();

            _filter.AddListener(this);
        }


        public void OnEntityAdded(in EcsEntity entity)
        {
            var tip = View.toolTipCollection.Create();

            var itemType = entity.Get<ItemTypeComponent>().Value;
            
            tip.text.SetText(itemType.ToString());

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

                var rectTransform = (RectTransform)tip.transform;
                
                var transformedPos = transform.TransformPoint(Vector3.zero);
                rectTransform.position = RectTransformUtility.WorldToScreenPoint(camera, transformedPos);
            }
        }

        public void Dispose()
        {
            _filter.RemoveListener(this);
        }
    }
}