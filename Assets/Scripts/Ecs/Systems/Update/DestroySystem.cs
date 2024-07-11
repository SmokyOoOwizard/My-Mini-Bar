using Ecs.Components;
using Ecs.Components.Refs;
using Ecs.Core;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems.Update
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