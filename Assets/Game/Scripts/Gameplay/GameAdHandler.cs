using System;
using Game.Scripts.Core.Ads;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class GameAdHandler : IInitializable, IDisposable
    {
        private readonly AdService _adService;
        private readonly SignalBus _signalBus;

        public GameAdHandler(SignalBus signalBus, AdService adService)
        {
            _signalBus = signalBus;
            _adService = adService;
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
            _adService.ShowInterstitial();
        }
    }
}