using Ecs.Components.Input;
using Ecs.Core;
using Ecs.Worlds;
using Leopotam.Ecs;

namespace Ecs.Systems.Initialize
{
    public class InputInitializeSystem : IEcsInitSystem
    {
        private readonly GameEcsWorld _ecsWorld;

        public InputInitializeSystem(GameEcsWorld ecsWorld)
        {
            _ecsWorld = ecsWorld;
        }

        public void Init()
        {
            _ecsWorld.NewEntity().Get<MoveDirectionComponent>();
        }
    }
}