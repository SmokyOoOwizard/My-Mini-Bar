using Ecs.Components;
using Ecs.Components.Refs;
using Ecs.Views;
using Leopotam.Ecs;

namespace Ecs.Systems.Initialize
{
    public class PlayerInitializeSystem : IEcsInitSystem
    {
        private readonly EcsWorld _world;
        private readonly PlayerView _playerView;

        public PlayerInitializeSystem(
            EcsWorld world,
            PlayerView playerView
        )
        {
            _world = world;
            _playerView = playerView;
        }

        public void Init()
        {
            var playerEntity = _world.NewEntity();

            playerEntity.Get<PlayerComponent>();

            playerEntity.Get<TransformRefComponent>().Value = _playerView.transform;
        }
    }
}