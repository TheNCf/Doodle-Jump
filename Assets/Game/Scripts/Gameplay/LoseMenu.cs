using System;
using Game.Scripts.Core.Animation;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class LoseMenu : IInitializable, IDisposable
    {
        private readonly AnimationStarter _animationStarter;
        private readonly SignalBus _signalBus;
        private readonly LoseMenuView _view;

        public LoseMenu(LoseMenuView view, SignalBus signalBus, AnimationStarter animationStarter)
        {
            _view = view;
            _signalBus = signalBus;
            _animationStarter = animationStarter;
        }

        public void Dispose()
        {
            _signalBus.Unsubscribe<LoseSignal>(OnLose);
        }

        public void Initialize()
        {
            _signalBus.Subscribe<LoseSignal>(OnLose);
        }

        private void OnLose()
        {
            _animationStarter.Play(_view.Animatables, () => { }, _view);
        }
    }
}