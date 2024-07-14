using Leopotam.Ecs;

namespace Ecs.Core
{
    public class SystemInstaller
    {
        public EcsWorld World { get; protected set; }
        public IEcsSystem System { get; protected set; }
    }
    
    public class SystemInstaller<TWorld, TSystem> : SystemInstaller where TSystem : IEcsSystem where TWorld : EcsWorld
    {
        public SystemInstaller(TWorld world, TSystem system)
        {
            World = world;
            System = system;
        }   
    }
}