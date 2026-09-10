using Zenject;

namespace Game.Scripts.Gameplay
{
    public class GameplayInstaller : Installer<GameplayInstaller>
    {
        public override void InstallBindings()
        {
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
    }
}