using Ecs.Core;
using Ecs.Game.Components;
using Ecs.Game.Components.Refs;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Game.Systems.Update
{
    public class DestroySystem : IUpdateEcsSystem
    {
        private EcsFilter<DestroyedComponent> _filter;

        public void Run()
        {
            foreach (var entityId in _filter)
            {
                var entity = _filter.GetEntity(entityId);

                if (entity.Has<TransformRefComponent>())
                {
                    var transform = entity.Get<TransformRefComponent>().Value;
                    Object.Destroy(transform.gameObject);
                }

                entity.Destroy();
            }
        }
    }
}