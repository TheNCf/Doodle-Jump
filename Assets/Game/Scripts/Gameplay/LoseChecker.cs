using System;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class LoseChecker : IDisposable
    {
        private readonly LoseCheckerView _loseCheckerView;
        private readonly SignalBus _signalBus;

        private bool _isGameEnded;

        public LoseChecker(LoseCheckerView loseCheckerView, SignalBus signalBus)
        {
            _loseCheckerView = loseCheckerView;
            _signalBus = signalBus;

            _loseCheckerView.Lose += OnLose;
        }

        public void Dispose()
        {
            _loseCheckerView.Lose -= OnLose;
        }

        private void OnLose()
        {
            if (_isGameEnded)
                return;

            _signalBus.Fire(new LoseSignal());
            _isGameEnded = true;
        }
    }
}