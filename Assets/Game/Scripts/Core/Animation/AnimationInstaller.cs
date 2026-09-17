using Zenject;

namespace Game.Scripts.Core.Animation
{
    public class AnimationInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            BindAnimationStarter();
        }

        private void BindAnimationStarter()
        {
            Container
                .BindInterfacesAndSelfTo<AnimationStarter>()
                .AsSingle()
                .NonLazy();
        }
    }
}