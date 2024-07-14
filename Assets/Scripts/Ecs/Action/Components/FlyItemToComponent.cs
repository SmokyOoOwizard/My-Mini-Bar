using Ecs.Utils;
using UnityEngine;

namespace Ecs.Action.Components
{
    public struct FlyItemToComponent
    {
        public EntityId Item;
        public Transform Target;
        public Vector3 Offset;
    }
}