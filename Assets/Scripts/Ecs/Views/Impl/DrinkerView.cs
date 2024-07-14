using Ecs.Game.Components;
using Leopotam.Ecs;

namespace Ecs.Views.Impl
{
    public class DrinkerView : ItemReceiverSlotView
    {
        public float drinkDuration = 3f;
        
        public override void Init(EcsEntity entity, EcsWorld world)
        {
            base.Init(entity, world);
            
            entity.Get<DrinkerComponent>();
            entity.Get<DrinkDurationComponent>().Value = drinkDuration;
        }
    }
}