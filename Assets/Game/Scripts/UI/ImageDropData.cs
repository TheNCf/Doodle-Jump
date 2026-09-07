using UnityEngine;

namespace Game.Scripts.UI
{
    [CreateAssetMenu(fileName = "Image Drop Data", menuName = "Doodle Jump/Image Drop Data")]
    public class ImageDropData : ScriptableObject
    {
        [field: SerializeField] public float Delay { get; private set; }
        [field: SerializeField] public float StartSize { get; private set; }
        [field: SerializeField] public float MinSize { get; private set; }
        [field: SerializeField] public float EndSize { get; private set; }
        [field: SerializeField] public float ShrinkDuration { get; private set; }
        [field: SerializeField] public float InflateDuration { get; private set; }
    }
}