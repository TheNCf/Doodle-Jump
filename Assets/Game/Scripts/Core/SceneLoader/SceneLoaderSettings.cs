using UnityEngine;

namespace Game.Scripts.Core.SceneLoader
{
    [CreateAssetMenu(fileName = "Scene Loader Settings", menuName = "Doodle Jump/Scene Loader Settings")]
    public class SceneLoaderSettings : ScriptableObject
    {
        [field: SerializeField] public string SceneToLoad { get; private set; }
    }
}