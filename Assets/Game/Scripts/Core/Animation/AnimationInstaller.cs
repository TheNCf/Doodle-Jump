using Game.Scripts.Core.Animation.Effectors;
using Zenject;

namespace Game.Scripts.Core.Animation
{
    public class AnimationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindRectTransformDropEffector();
            BindImageFadeEffector();
            BindRectTransformHorizontalEffector();
        }

        private void BindRectTransformHorizontalEffector()
        {
            Container
                .Bind<RectTransformHorizontalMoveEffector>()
                .AsSingle()
                .NonLazy();
        }

        private void BindImageFadeEffector()
        {
            Container
                .Bind<ImageFadeEffector>()
                .AsSingle()
                .NonLazy();
        }

        private void BindRectTransformDropEffector()
        {
            Container
                .BindInterfacesAndSelfTo<RectTransformDropEffector>()
                .AsSingle()
                .NonLazy();
        }
    }
}