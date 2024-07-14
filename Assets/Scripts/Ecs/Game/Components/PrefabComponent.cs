using UnityEngine;

namespace Ecs.Game.Components
{
    public struct PrefabComponent<T> where T : MonoBehaviour
    {
        public T Value;
    }
}