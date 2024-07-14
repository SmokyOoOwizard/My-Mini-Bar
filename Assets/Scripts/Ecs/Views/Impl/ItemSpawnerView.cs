using Ecs.Game.Components;
using Ecs.Game.Components.Items;
using Ecs.Game.Components.Refs;
using Ecs.Game.Components.Spawner;
using Ecs.Game.Components.Timer;
using Ecs.Utils;
using Leopotam.Ecs;

namespace Ecs.Views.Impl
{
    public class ItemSpawnerView : AEntityView
    {
        public float timer;

        public ItemSpawnerSlot[] slots;

        public ItemView itemPrefab;

        public override void Init(EcsEntity entity, EcsWorld world)
        {
            entity.Get<ItemSpawnerComponent>();

            entity.Get<TransformRefComponent>().Value = transform;

            entity.Get<TimerComponent>().Value = timer;

            entity.Get<TimerLeftComponent>().Value = timer;

            entity.Get<PrefabComponent<ItemView>>().Value = itemPrefab;
            
            entity.Get<ItemTypeComponent>().Value = itemPrefab.Type;

            var slotsRef = entity.Get<ItemSlotsComponent>().Value;
            var freeSlotsRef = entity.Get<ItemFreeSlotsComponent>().Value;

            foreach (var slotView in slots)
            {
                var slotEntity = world.NewEntity();

                slotView.Init(slotEntity, world);

                var packedSlot = slotEntity.Pack();

                slotsRef.Add(packedSlot);
                freeSlotsRef.Add(packedSlot);
            }
        }
    }
}