using Ecs.Components.Items;
using Ecs.Components.Parameters;
using Ecs.Components.Refs;
using Ecs.Utils;
using Leopotam.Ecs;

namespace Ecs.Views.Impl
{
    public class ItemView : AEntityView
    {
        public EItemType Type; 
        public float Height;
        
        public override void Init(EcsEntity entity, EcsWorld world)
        {
            gameObject.Link(entity);

            entity.Get<TransformRefComponent>().Value =transform;
            entity.Get<HeightComponent>().Value = Height;
            entity.Get<ItemTypeComponent>().Value = Type;
        }
    }
}