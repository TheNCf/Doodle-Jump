using Zenject;

namespace Game.Scripts.UI.ViewModels
{
    public class ViewModelsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindScoreViewModel();
        }

        private void BindScoreViewModel()
        {
            Container
                .BindInterfacesAndSelfTo<ScoreViewModel>()
                .AsSingle()
                .NonLazy();
        }
    }
}