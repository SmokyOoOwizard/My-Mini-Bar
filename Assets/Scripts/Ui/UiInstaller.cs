using SimpleUi;
using SimpleUi.Interfaces;
using SimpleUi.Signals;
using Ui.DrinkerToolTip;
using Ui.Inventory;
using Ui.ItemSpawnerToolTip;
using Ui.Joystick;
using Ui.TrashCanToolTip;
using Ui.Windows;
using UnityEngine;
using Zenject;

namespace Ui
{
    public class UiInstaller : MonoInstaller
    {
        [SerializeField]
        private Canvas canvas;

        [SerializeField]
        private JoystickView joystickView;
        [SerializeField]
        private ItemSpawnerToolTipsView spawnerToolTipsView;
        [SerializeField]
        private DrinkerToolTipsView drinkerToolTipsView;
        [SerializeField]
        private InventoryView inventoryView;
        [SerializeField]
        private TrashCanToolTipsView trashCanToolTipsView;

        public override void InstallBindings()
        {
            BindWindows();
            BindWidgets();

            SignalBusInstaller.Install(Container);
        }

        private void BindWindows()
        {
            Container.BindUiSignals(EWindowLayer.Local);
            Container.BindWindowsController<WindowsController>(EWindowLayer.Local);

            Container.BindInterfacesAndSelfTo<GameHudWindow>().AsSingle();
        }

        private void BindWidgets()
        {
            var canvasView = Container.InstantiatePrefabForComponent<Canvas>(canvas);
            var canvasTransform = canvasView.transform;

            Container.Bind<IUiFilter>().To<CustomGraphicRaycaster>().FromComponentOn(canvas.gameObject).AsSingle();
            
            Container.BindUiView<JoystickController, JoystickView>(joystickView, canvasTransform);
            
            Container.BindUiView<ItemSpawnerToolTipsController, ItemSpawnerToolTipsView>(spawnerToolTipsView, canvasTransform);
            Container.BindUiView<DrinkerToolTipsController, DrinkerToolTipsView>(drinkerToolTipsView, canvasTransform);
            Container.BindUiView<InventoryController, InventoryView>(inventoryView, canvasTransform);
            Container.BindUiView<TrashCanToolTipsController, TrashCanToolTipsView>(trashCanToolTipsView, canvasTransform);
        }
    }
}