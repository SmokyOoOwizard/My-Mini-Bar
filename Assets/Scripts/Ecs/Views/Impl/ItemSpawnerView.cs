using Ecs.Components;
using Ecs.Components.Refs;
using Ecs.Components.Spawner;
using Ecs.Components.Timer;
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