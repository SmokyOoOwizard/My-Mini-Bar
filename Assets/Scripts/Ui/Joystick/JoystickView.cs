using SimpleUi.Abstracts;
using Ui.Utils;
using UnityEngine;
using UnityEngine.UI;

namespace Ui.Joystick
{
    public class JoystickView : UiView
    {
        public OnPointerHandler touchZone;
        public Image background;
        public Image handle;

        public float radius;

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(background.transform.position, radius);
        }
    }
}