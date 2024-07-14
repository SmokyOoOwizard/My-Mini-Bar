using Ecs.Core;
using Ecs.Game.Components;
using Ecs.Game.Components.Input;
using Ecs.Game.Components.Parameters;
using Ecs.Game.Components.Refs;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Game.Systems.Update.Player
{
    public class PlayerMovementSystem : IFixedUpdateEcsSystem
    {
        private EcsFilter<MoveDirectionComponent> _moveFilter;
        private EcsFilter<ViewRefComponent<CharacterController>, SpeedComponent, PlayerComponent> _playerFilter;

        public void Run()
        {
            if (_moveFilter.IsEmpty())
                return;

            var moveDirection = _moveFilter.Get1(0);

            foreach (var entityId in _playerFilter)
            {
                var speed = _playerFilter.Get2(entityId).Value;

                var delta = moveDirection.Value * Time.deltaTime * speed;

                var cc = _playerFilter.Get1(entityId).Value;

                cc.Move(new Vector3(delta.x, 0, delta.y));
            }
        }
    }
}