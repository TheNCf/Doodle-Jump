using System;
using Game.Scripts.Core.Animation;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class LoseMenu : IInitializable, IDisposable
    {
        private LoseMenuView _view;
        private SignalBus _signalBus;

        public LoseMenu(LoseMenuView view, SignalBus signalBus)
        {
            _view = view;
            _signalBus = signalBus;
        }

        public void Initialize()
        {
            _signalBus.Subscribe<LoseSignal>(OnLose);
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<LoseSignal>(OnLose);
        }

        private void OnLose()
        {
            IAnimatable<IAnimationStarter> animatable = _view.Animatable.Interface;
            animatable.AnimationStarter.Activate(animatable);
        }
    }
}