using Core;
using Leopotam.Ecs;

namespace Ecs.Systems.Initialize
{
    public class UiInitializeSystem : IEcsInitSystem
    {
        private readonly IUiInitializable[] _ui;

        public UiInitializeSystem(IUiInitializable[] ui)
        {
            _ui = ui;
        }
        
        public void Init()
        {
            foreach (var ui in _ui)
            {
                ui.Initialize();
            }
        }
    }
}