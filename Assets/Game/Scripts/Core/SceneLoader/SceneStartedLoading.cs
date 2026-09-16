namespace Game.Scripts.Core.SceneLoader
{
    public class SceneStartedLoading
    {
        public SceneName SceneName { get; }

        public SceneStartedLoading(SceneName sceneName)
        {
            SceneName = sceneName;
        }
    }
}