using UnityEngine;

namespace Ecs.Views
{
    public class ItemSpawnerView : MonoBehaviour
    {
        public float timer;
        
        public ItemSpawnerSlot[] slots;
        
        public ItemView itemPrefab;
    }
}