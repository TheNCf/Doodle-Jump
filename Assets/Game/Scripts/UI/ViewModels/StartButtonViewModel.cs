using Game.Scripts.Gameplay.Signals;
using MVVM;
using Zenject;

namespace Game.Scripts.UI.ViewModels
{
    public class StartButtonViewModel
    {
        private SignalBus _signalBus;
        
        public StartButtonViewModel(SignalBus signalBus)
        {
            _signalBus = signalBus;
        }

        [Method("OnStartClick")]
        public void OnStartClicked()
        {
            _signalBus.Fire(new OpenGameSceneSignal());
        }
    }
}