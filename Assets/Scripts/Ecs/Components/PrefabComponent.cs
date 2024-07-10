using UnityEngine;

namespace Ecs.Components
{
    public struct PrefabComponent<T> where T : MonoBehaviour
    {
        public T Value;
    }
}