using System.Collections.Generic;
using Ecs.Components;
using Ecs.Components.Camera;
using Ecs.Components.Parameters;
using Ecs.Components.Refs;
using Ecs.Utils;
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
            _playerView.gameObject.Link(playerEntity);

            playerEntity.Get<PlayerComponent>();

            playerEntity.Get<TransformRefComponent>().Value = _playerView.transform;

            playerEntity.Get<SpeedComponent>().Value = _playerView.Speed;

            playerEntity.Get<CameraTargetComponent>();

            playerEntity.Get<ViewRefComponent<PlayerView>>().Value = _playerView;

            playerEntity.Get<StackInventoryComponent>();
            
            playerEntity.Get<PickUpDistanceComponent>().Value = _playerView.PickUpDistance;

            playerEntity.Get<StackInventoryParentComponent>().Value = _playerView.stackInventoryParent;
            playerEntity.Get<StackInventoryHeightComponent>();
        }
    }
}