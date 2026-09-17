namespace Game.Scripts.Core.SceneLoader
{
    public class SceneStartedLoading
    {
        public SceneStartedLoading(SceneName sceneName)
        {
            SceneName = sceneName;
        }

        public SceneName SceneName { get; }
    }
}