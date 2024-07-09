using Ui.Windows;
using UnityEngine;
using Zenject;

public class UiInstaller : MonoInstaller
{
    [SerializeField] private Canvas canvas;
    
    public override void InstallBindings()
    {
        BindWindows();
        BindWidgets();
    }

    private void BindWindows()
    {
        Container.BindInterfacesAndSelfTo<GameHudWindow>().AsSingle();
    }

    private void BindWidgets()
    {
        var canvasView = Container.InstantiatePrefabForComponent<Canvas>(canvas);
        var canvasTransform = canvasView.transform;
    }
}