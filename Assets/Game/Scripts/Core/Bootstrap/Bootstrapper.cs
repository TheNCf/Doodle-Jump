using Game.Scripts.Core.Animation;
using Game.Scripts.Core.SceneLoader;
using Zenject;

namespace Game.Scripts.Core.Bootstrap
{
    public class Bootstrapper : IInitializable
    {
        private SceneLoader.SceneLoader _sceneLoader;
        private SceneLoaderSettings _settings;
        private BootstrapView _bootstrapView;
        private AnimationStarter _animationStarter;

        public Bootstrapper(SceneLoaderSettings settings, SceneLoader.SceneLoader sceneLoader,
            BootstrapView bootstrapView, AnimationStarter animationStarter)
        {
            _sceneLoader = sceneLoader;
            _bootstrapView = bootstrapView;
            _animationStarter = animationStarter;
            _settings = settings;
        }

        public void Initialize()
        {
            _sceneLoader.StartSceneLoading(_settings.SceneToLoad);

            _animationStarter.Play(_bootstrapView.Animatables, () => _sceneLoader.AllowSceneActivation(),
                _bootstrapView);
        }
    }
}