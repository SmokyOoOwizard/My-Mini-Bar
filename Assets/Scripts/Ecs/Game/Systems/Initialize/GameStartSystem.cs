﻿using System;
using Leopotam.Ecs;
using SimpleUi.Signals;
using Ui.Windows;
using UniRx;
using Zenject;

namespace Ecs.Game.Systems.Initialize
{
    public class GameStartSystem : IEcsInitSystem
    {
        private readonly SignalBus _signalBus;

        public GameStartSystem(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        public void Init()
        {
            // tmp
            Observable.TimerFrame(1).Subscribe(_ => _signalBus.OpenWindow<GameHudWindow>());
        }
    }
}