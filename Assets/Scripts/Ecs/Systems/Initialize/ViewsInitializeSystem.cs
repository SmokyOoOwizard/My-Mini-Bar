using Ecs.Core;
using Ecs.Views;
using Ecs.Worlds;
using Leopotam.Ecs;

namespace Ecs.Systems.Initialize
{
    public class ViewsInitializeSystem : IEcsInitSystem
    {
        private readonly GameEcsWorld _world;
        private readonly AEntityView[] _views;

        public ViewsInitializeSystem(
            GameEcsWorld world,
            AEntityView[] views
        )
        {
            _world = world;
            _views = views;
        }

        public void Init()
        {
            foreach (var view in _views)
            {
                var entity = _world.NewEntity();

                view.Init(entity, _world);
            }
        }
    }
}