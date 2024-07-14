using Ecs.Core;
using Ecs.Systems.Update;
using Ecs.Systems.Update.Collectables;
using Ecs.Worlds;

namespace Ecs
{
    public class ActionWorldEcsInstaller : AEcsInstaller<ActionEcsWorld>
    {
        public override void InstallBindings()
        {
            BindUpdateSystems();
        }

        private void BindUpdateSystems()
        {
            BindSystem<CollectItemSystem>();
            BindSystem<CollectItemFlyIntoStackSystem>();
            
            BindSystem<DropItemSystem>();
            
            BindSystem<FlyItemToSystem>();
            BindSystem<ActionDestroyerSystem>();
        }
    }
}