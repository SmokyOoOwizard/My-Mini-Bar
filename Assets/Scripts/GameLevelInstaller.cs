using Ecs.Views;
using UnityEngine;
using Zenject;

public class GameLevelInstaller : MonoInstaller<GameLevelInstaller>
{
    [SerializeField]
    private PlayerView player;

    public override void InstallBindings()
    {
        Container.BindInstance(player).AsSingle();
    }
}