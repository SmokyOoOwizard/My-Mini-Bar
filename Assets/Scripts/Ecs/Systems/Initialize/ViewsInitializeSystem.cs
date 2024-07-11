using Ecs.Views;
using Leopotam.Ecs;

namespace Ecs.Systems.Initialize
{
    public class ViewsInitializeSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world;
        private readonly AEntityView[] _views;

        public ViewsInitializeSystem(
            EcsWorld world,
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