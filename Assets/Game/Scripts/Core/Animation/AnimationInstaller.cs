using Game.Scripts.Core.Animation.Effectors;
using Zenject;

namespace Game.Scripts.Core.Animation
{
    public class AnimationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindImageDropEffector();
            BindImageFadeEffector();
        }

        private void BindImageFadeEffector()
        {
            Container
                .Bind<ImageFadeEffector>()
                .AsSingle()
                .NonLazy();
        }

        private void BindImageDropEffector()
        {
            Container
                .BindInterfacesAndSelfTo<ImageDropEffector>()
                .AsSingle()
                .NonLazy();
        }
    }
}