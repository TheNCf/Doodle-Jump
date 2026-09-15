namespace Game.Scripts.Core.SceneLoader
{
    public class SceneStartedLoading
    {
        public string SceneName { get; }

        public SceneStartedLoading(string sceneName)
        {
            SceneName = sceneName;
        }
    }
}