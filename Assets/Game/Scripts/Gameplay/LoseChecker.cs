using Zenject;

namespace Game.Scripts.Gameplay
{
    public class LoseChecker : ITickable
    {
        private PlayerCharacterView _playerCharacterView;
        private LoseCheckerView _loseCheckerView;
        private SignalBus _signalBus;

        public LoseChecker(PlayerCharacterView playerCharacterView, LoseCheckerView loseCheckerView, SignalBus signalBus)
        {
            _playerCharacterView = playerCharacterView;
            _loseCheckerView = loseCheckerView;
            _signalBus = signalBus;
        }

        public void Tick()
        {
            if (_playerCharacterView.Transform.position.y < _loseCheckerView.Transform.position.y)
            {
                _signalBus.Fire(new LoseSignal());
            }
        }
    }
}