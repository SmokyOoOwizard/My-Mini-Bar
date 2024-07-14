using UnityEngine;

namespace Utils
{
    public static class AnimationKeys
    {
        public static readonly int Walk = Animator.StringToHash("Walk");
        public static readonly int Carrying = Animator.StringToHash("Carrying");
        public static readonly int PickUp = Animator.StringToHash("PickUp");
        public static readonly int Drink = Animator.StringToHash("Drink");
    }
}