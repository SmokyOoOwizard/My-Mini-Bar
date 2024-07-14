using Ecs.Action.Systems;
using Ecs.Core;
using Ecs.Game.Systems.Update;
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