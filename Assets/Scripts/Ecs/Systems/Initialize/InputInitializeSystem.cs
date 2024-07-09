using Ecs.Components.Input;
using Leopotam.Ecs;

namespace Ecs.Systems.Initialize
{
    public class InputInitializeSystem : IEcsInitSystem
    {
        private readonly EcsWorld _ecsWorld;

        public InputInitializeSystem(EcsWorld ecsWorld)
        {
            _ecsWorld = ecsWorld;
        }

        public void Init()
        {
            _ecsWorld.NewEntity().Get<MoveDirectionComponent>();
        }
    }
}