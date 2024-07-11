using Leopotam.Ecs;

namespace Ecs.Utils
{
    public static class EcsWorldFilterExtensions
    {
        public static T GetFilter<T>(this EcsWorld world) where T : EcsFilter
        {
            return (T)world.GetFilter(typeof(T));
        }
    }
}