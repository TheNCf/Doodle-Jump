using System;
using Game.Scripts.Core.Ads;
using Game.Scripts.Core.Animation;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class LoseMenu : IInitializable, IDisposable
    {
        private LoseMenuView _view;
        private SignalBus _signalBus;
        private AnimationStarter _animationStarter;
        private AdService _adService;

        public LoseMenu(LoseMenuView view, SignalBus signalBus, AnimationStarter animationStarter, AdService adService)
        {
            _view = view;
            _signalBus = signalBus;
            _animationStarter = animationStarter;
            _adService = adService;
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
            _animationStarter.Play(_view.Animatables, () => { }, _view);
            _adService.ShowInterstitial();
        }
    }
}