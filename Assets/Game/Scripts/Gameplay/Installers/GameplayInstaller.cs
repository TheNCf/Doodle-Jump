using Zenject;

namespace Game.Scripts.Gameplay.Installers
{
    public class GameplayInstaller : Installer<PlayerCharacterView, GameplayInstaller>
    {
        private readonly PlayerCharacterView _playerCharacterViewPrefab;

        public GameplayInstaller(PlayerCharacterView playerCharacterViewPrefab)
        {
            _playerCharacterViewPrefab = playerCharacterViewPrefab;
        }

        public override void InstallBindings()
        {
            BindPlayerCharacterView();
            BindPlayerCharacterMover();
            BindPlayerCharacterBouncer();
            BindMovingBehaviour();
            BindScoreCounter();
        }

        private void BindScoreCounter()
        {
            Container
                .BindInterfacesAndSelfTo<ScoreCounter>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerCharacterBouncer()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerCharacterBouncer>()
                .AsSingle()
                .NonLazy();
        }

        private void BindPlayerCharacterMover()
        {
            Container
                .BindInterfacesAndSelfTo<PlayerCharacterMover>()
                .AsSingle()
                .NonLazy();
        }

        private void BindMovingBehaviour()
        {
            Container
                .Bind<MovingBehaviour>()
                .AsTransient();
        }

        private void BindPlayerCharacterView()
        {
            Container
                .Bind<PlayerCharacterView>()
                .FromComponentInNewPrefab(_playerCharacterViewPrefab)
                .AsSingle()
                .NonLazy();
        }
    }
}