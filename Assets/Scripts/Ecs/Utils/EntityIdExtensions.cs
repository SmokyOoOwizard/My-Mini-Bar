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

        public static EcsEntity? Unpack(this EntityId id, EcsWorld world)
        {
            var entity = world.RestoreEntityFromInternalId(id.Id, id.Gen);
            if (!entity.IsAlive())
                return null;

            return entity;
        }
    }
}