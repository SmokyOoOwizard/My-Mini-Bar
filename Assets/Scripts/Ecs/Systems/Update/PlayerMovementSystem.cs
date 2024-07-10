using Ecs.Components;
using Ecs.Components.Input;
using Ecs.Components.Parameters;
using Ecs.Components.Refs;
using Ecs.Core;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Systems.Update
{
    public class PlayerMovementSystem : IUpdateEcsSystem
    {
        private EcsFilter<MoveDirectionComponent> _moveFilter;
        private EcsFilter<TransformRefComponent, SpeedComponent, PlayerComponent> _playerFilter;
        
        public void Run()
        {
            if (_moveFilter.IsEmpty())
                return;
            
            var moveDirection = _moveFilter.Get1(0);
            
            foreach (var entity in _playerFilter)
            {
                var transform = _playerFilter.Get1(entity).Value;

                var speed = _playerFilter.Get2(entity).Value;
                
                var delta= moveDirection.Value * Time.deltaTime * speed;

                transform.position += new Vector3(delta.x, 0, delta.y);
            }
        }
    }
}