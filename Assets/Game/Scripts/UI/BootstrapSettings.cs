using Game.Scripts.UI.Views;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Game.Scripts.UI
{
    [CreateAssetMenu(fileName = "Bootstrap Settings", menuName = "Doodle Jump/Bootstrap Settings")]
    public class BootstrapSettings : ScriptableObject
    {
        [field: SerializeField] public string SceneToLoad { get; private set; }
    }
}