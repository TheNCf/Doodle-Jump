using Game.Scripts.Core;
using Game.Scripts.Gameplay.LevelGeneration;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class InfrastructureInstaller : Installer<GameBalance, InfrastructureInstaller>
    {
        private readonly GameBalance _gameBalance;
        
        public InfrastructureInstaller(GameBalance gameBalance)
        {
            _gameBalance = gameBalance;
        }
        
        public override void InstallBindings()
        {
            BindInputService();
            BindGameBalance();
        }
        
        private void BindGameBalance()
        {
            Container.BindInterfacesAndSelfTo<GameBalance>()
                .FromInstance(_gameBalance)
                .AsSingle()
                .NonLazy();
        }

        private void BindInputService()
        {
            Container
                .BindInterfacesAndSelfTo<InputService>()
                .AsSingle()
                .NonLazy();
        }
    }
}