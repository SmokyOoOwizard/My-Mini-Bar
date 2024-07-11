using Ecs.Utils;
using UnityEngine;

namespace Ecs.Views
{
    public class ItemReceiverSlotView : MonoBehaviour
    {
        public float pickUpDistance = 0.5f;
        public EItemFilter filter = EItemFilter.Any;
    }
}