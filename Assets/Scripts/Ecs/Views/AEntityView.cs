using Leopotam.Ecs;
using UnityEngine;

namespace Ecs.Views
{
    public abstract class AEntityView : MonoBehaviour
    {
        public abstract void Init(EcsEntity entity, EcsWorld world);
    }
}