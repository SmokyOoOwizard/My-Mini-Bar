using Leopotam.Ecs;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

namespace Ecs.Utils
{
    public static class GameObjectLinkerExtension
    {
        public static void Link(this GameObject obj, EcsEntity entity)
        {
            var packedEntity = entity.Pack();
            var world = entity.GetInternalWorld();
            obj.OnDestroyAsObservable().Subscribe(_ =>
            {
                if (!packedEntity.TryUnpack(world, out var unpackedEntity))
                    return;

                unpackedEntity.Destroy();
            });
        }
    }
}