using SimpleUi;
using Ui.Joystick;

namespace Ui.Windows
{
    public class GameHudWindow : WindowBase
    {
        public override string Name => nameof(GameHudWindow);

        protected override void AddControllers()
        {
            AddController<JoystickController>();
        }
    }
}