using System.Collections.Generic;
using Ecs.Views;
using UnityEngine;
using Zenject;

public class GameLevelInstaller : MonoInstaller<GameLevelInstaller>
{
    [SerializeField]
    private List<AEntityView> views;

    public override void InstallBindings()
    {
        foreach (var view in views)
        {
            Container.Bind(view.GetType(), typeof(AEntityView)).FromInstance(view);
        }
    }
}