using Ecs.Components;
using Ecs.Components.Items;
using Ecs.Components.Refs;
using Ecs.Components.Spawner;
using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Views.Impl
{
    public class ItemSpawnerSlot : AEntityView
    {
        public Transform spawnPoint;
        
        public override void Init(EcsEntity entity, EcsWorld world)
        {
            entity.Get<TransformRefComponent>().Value = transform;
            entity.Get<ItemSlotComponent>();
            entity.Get<SenderComponent>();
            entity.Get<SpawnPointComponent>().Value = spawnPoint;
        }
    }
}