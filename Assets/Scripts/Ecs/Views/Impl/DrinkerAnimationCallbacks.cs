using UniRx;
using UnityEngine;

namespace Ecs.Views.Impl
{
    public class DrinkerAnimationCallbacks : MonoBehaviour
    {
        public ReactiveCommand PickedUpCmd { get; } = new();
        public ReactiveCommand PickUpCmd { get; } = new();
        public ReactiveCommand DrinkedCmd { get; } = new();

        void PickedUp(AnimationEvent animationEvent)
        {
            PickedUpCmd.Execute();
        }
        
        void PickUp(AnimationEvent animationEvent)
        {
            PickUpCmd.Execute();
        }

        void Drinked(AnimationEvent animationEvent)
        {
            DrinkedCmd.Execute();
        }
    }
}