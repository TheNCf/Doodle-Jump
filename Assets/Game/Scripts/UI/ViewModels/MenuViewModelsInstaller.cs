using Zenject;

namespace Game.Scripts.UI.ViewModels
{
    public class MenuViewModelsInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindStartButtonViewModel();
        }

        private void BindStartButtonViewModel()
        {
            Container.Bind<StartButtonViewModel>()
                .AsSingle()
                .NonLazy();
        }
    }
}