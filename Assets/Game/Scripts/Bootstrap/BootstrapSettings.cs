using UnityEngine;

namespace Game.Scripts.Bootstrap
{
    [CreateAssetMenu(fileName = "Bootstrap Settings", menuName = "Doodle Jump/Bootstrap Settings")]
    public class BootstrapSettings : ScriptableObject
    {
        [field: SerializeField] public string SceneToLoad { get; private set; }
    }
}