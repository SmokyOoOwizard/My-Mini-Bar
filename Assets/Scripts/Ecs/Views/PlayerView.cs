using UnityEngine;

namespace Ecs.Views
{
    public class PlayerView : MonoBehaviour
    {
        public float Speed;
        public float PickUpDistance;
        public int MaxStackSize;
        public Transform stackInventoryParent;
    }
}