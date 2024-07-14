using System;
using Ecs.Game;
using Ecs.Game.Components;
using Ecs.Game.Components.Inventories;
using Ecs.Game.Components.Items;
using Ecs.Utils;
using Ecs.Worlds;
using Leopotam.Ecs;
using SimpleUi.Abstracts;
using UnityEngine;
using UnityEngine.Pool;

namespace Ui.Inventory
{
    public class InventoryController : UiController<InventoryView>,
                                       IEcsFilterListener,
                                       IDisposable
    {
        private readonly GameEcsWorld _world;

        private readonly EcsFilter<
            StackInventoryComponent,
            PlayerComponent,
            InventoryUpdatedComponent,
            ViewInitedComponent
        > _filter;

        public InventoryController(
            GameEcsWorld world
        )
        {
            _world = world;
            _filter = world.GetFilter<EcsFilter<
                StackInventoryComponent,
                PlayerComponent,
                InventoryUpdatedComponent,
                ViewInitedComponent
            >>();


            _filter.AddListener(this);
        }


        public void OnEntityAdded(in EcsEntity entity)
        {
            var inventory = entity.Get<StackInventoryComponent>().Value;
            var itemsCount = inventory.Count;

            Debug.Log("INVENTORYYY");
            
            ResizeViewCollection(itemsCount);

            using var viewEnumerator = View.itemsCollection.GetEnumerator();
            foreach (var packedItem in inventory)
            {
                if (!packedItem.TryUnpack(_world, out var itemEntity))
                    continue;

                var itemType = itemEntity.Get<ItemTypeComponent>().Value;

                viewEnumerator.MoveNext();

                var itemView = viewEnumerator.Current!;
                itemView.text.SetText(itemType.ToString());
            }
        }

        private void ResizeViewCollection(int itemsCount)
        {
            var viewExists = View.itemsCollection.Count;

            using var _ = ListPool<InventoryItemView>.Get(out var pool);

            if (viewExists > itemsCount)
            {
                var diff = viewExists - itemsCount;

                var i = 0;
                foreach (var itemView in View.itemsCollection)
                {
                    if (i >= diff)
                        break;
                    i++;

                    pool.Add(itemView);
                }

                foreach (var toDespawn in pool)
                {
                    View.itemsCollection.Despawn(toDespawn);
                }
            }
            else
            {
                for (int i = viewExists; i < itemsCount; i++)
                {
                    View.itemsCollection.Create();
                }
            }
        }

        public void OnEntityRemoved(in EcsEntity entity)
        {
        }

        public void Dispose()
        {
            _filter.RemoveListener(this);
        }
    }
}