using Ecs.Views;
using UnityEngine;
using Zenject;

public class GameLevelInstaller : MonoInstaller<GameLevelInstaller>
{
    [SerializeField]
    private PlayerView player;
    [SerializeField]
    private CameraView camera;

    public override void InstallBindings()
    {
        Container.BindInstance(player).AsSingle();
        Container.BindInstance(camera).AsSingle();
    }
}