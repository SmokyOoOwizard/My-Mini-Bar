﻿using System;
using System.Collections.Generic;
using Ecs.Game.Components;
using Ecs.Game.Components.Items;
using Ecs.Game.Components.Refs;
using Ecs.Game.Components.Spawner;
using Ecs.Utils;
using Ecs.Views.Impl;
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
            PrefabComponent<ItemView>,
            ItemTypeComponent
        > _filter;

        public ItemSpawnerToolTipsController(
            GameEcsWorld world
        )
        {
            _filter = world.GetFilter<EcsFilter<
                ItemSpawnerComponent,
                PrefabComponent<ItemView>,
                ItemTypeComponent
            >>();

            _filter.AddListener(this);
        }


        public void OnEntityAdded(in EcsEntity entity)
        {
            var tip = View.itemSpawnerToolTipCollection.Create();

            var itemType = entity.Get<ItemTypeComponent>().Value;

            tip.text.SetText(itemType.ToString());

            _activeTips[entity] = tip;
        }

        public void OnEntityRemoved(in EcsEntity entity)
        {
            if (!_activeTips.ContainsKey(entity))
                return;

            var tip = _activeTips[entity];

            View.itemSpawnerToolTipCollection.Despawn(tip);
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