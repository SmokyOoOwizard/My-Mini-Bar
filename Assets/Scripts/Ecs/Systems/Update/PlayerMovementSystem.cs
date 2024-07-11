using Ecs.Components;
using Ecs.Components.Input;
using Ecs.Components.Parameters;
using Ecs.Components.Refs;
using Ecs.Core;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems.Update
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