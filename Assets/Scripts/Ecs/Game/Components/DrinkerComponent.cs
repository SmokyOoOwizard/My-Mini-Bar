using Leopotam.Ecs;

namespace Ecs.Game.Components
{
    public struct DrinkerComponent : IEcsIgnoreInFilter
    {
    }
    
    public struct DrinkDurationComponent 
    {
        public float Value;
    }
    
    public struct DrinkProgressComponent 
    {
        public float Value;
    }
}