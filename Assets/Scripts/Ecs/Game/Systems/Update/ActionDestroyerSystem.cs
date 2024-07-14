using Ecs.Action.Components;
using Ecs.Core;
using Leopotam.Ecs;

namespace Ecs.Game.Systems.Update
{
    public class ActionDestroyerSystem : IUpdateEcsSystem
    {
        private EcsFilter<ActionComponent> _tweenFilter;

        public void Run()
        {
            foreach (var entityId in _tweenFilter)
            {
                var entity = _tweenFilter.GetEntity(entityId);
                entity.Destroy();
            }
        }
    }
}