using UniRx;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Ui.Utils
{
    public class OnPointerHandler : MonoBehaviour,
                                    IPointerDownHandler,
                                    IPointerUpHandler,
                                    IDragHandler
    {
        public ReactiveCommand<PointerEventData> OnPointerDownCmd { get; } = new();
        public ReactiveCommand<PointerEventData> OnPointerMoveCmd { get; } = new();
        public ReactiveCommand<PointerEventData> OnPointerUpCmd { get; } = new();

        public void OnPointerDown(PointerEventData eventData)
        {
            OnPointerDownCmd.Execute(eventData);
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            OnPointerUpCmd.Execute(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            OnPointerMoveCmd.Execute(eventData);
        }
    }
}