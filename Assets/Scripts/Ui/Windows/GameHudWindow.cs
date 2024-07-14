using SimpleUi;
using Ui.DrinkerToolTip;
using Ui.Inventory;
using Ui.ItemSpawnerToolTip;
using Ui.Joystick;
using Ui.TrashCanToolTip;

namespace Ui.Windows
{
    public class GameHudWindow : WindowBase
    {
        public override string Name => nameof(GameHudWindow);

        protected override void AddControllers()
        {
            AddController<InventoryController>();
            AddController<TrashCanToolTipsController>();
            AddController<ItemSpawnerToolTipsController>();
            AddController<DrinkerToolTipsController>();
            AddController<JoystickController>();
        }
    }
}