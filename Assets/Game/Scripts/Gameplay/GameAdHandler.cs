using System;
using Game.Scripts.Core.Ads;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class GameAdHandler : IInitializable, IDisposable
    {
        private SignalBus _signalBus;
        private AdService _adService;

        public GameAdHandler(SignalBus signalBus, AdService adService)
        {
            _signalBus = signalBus;
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
            _adService.ShowInterstitial();
        }
    }
}