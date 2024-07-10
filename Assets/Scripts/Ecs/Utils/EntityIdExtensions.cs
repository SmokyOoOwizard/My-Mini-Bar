using Leopotam.Ecs;

namespace Ecs.Utils
{
    public static class EntityIdExtensions
    {
        public static EntityId Pack(this EcsEntity entity)
        {
            return new EntityId
            {
                Id = entity.GetInternalId(),
                Gen = entity.GetInternalGen()
            };
        }

        public static bool TryUnpack(this EntityId id, EcsWorld world, out EcsEntity entity)
        {
            entity = world.RestoreEntityFromInternalId(id.Id, id.Gen);

            return entity.IsAlive();
        }
    }
}