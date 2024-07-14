using Ecs.Core;
using Ecs.Game.Components;
using Ecs.Game.Components.Input;
using Ecs.Game.Components.Parameters;
using Ecs.Game.Components.Refs;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Game.Systems.Update.Player
{
    public class PlayerRotationSystem : IFixedUpdateEcsSystem
    {
        private EcsFilter<MoveDirectionComponent> _moveFilter;
        private EcsFilter<TransformRefComponent, RotationSpeedComponent, PlayerComponent> _playerFilter;

        public void Run()
        {
            if (_moveFilter.IsEmpty())
                return;

            var moveDirection = _moveFilter.Get1(0).Value;
            if (moveDirection == Vector2.zero)
                return;

            foreach (var entityId in _playerFilter)
            {
                var speed = _playerFilter.Get2(entityId).Value;

                var transform = _playerFilter.Get1(entityId).Value;

                var rot = transform.rotation;
                var targetRot = Quaternion.LookRotation(new Vector3(moveDirection.x, 0, moveDirection.y));

                var finalRot = Quaternion.RotateTowards(rot, targetRot, speed * Time.deltaTime);

                transform.rotation = finalRot;
            }
        }
    }
}