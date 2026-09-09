using UnityEngine;

namespace Game.Scripts.Core.Animation
{
    [CreateAssetMenu(fileName = "Object Animation Data", menuName = "Doodle Jump/Object Animation Data")]
    public class ObjectAnimationData : ScriptableObject
    {
        [field: SerializeField] public float Delay { get; private set; }
        [field: SerializeField] public float StartValue { get; private set; }
        [field: SerializeField] public float MinValue { get; private set; }
        [field: SerializeField] public float EndValue { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
    }
}