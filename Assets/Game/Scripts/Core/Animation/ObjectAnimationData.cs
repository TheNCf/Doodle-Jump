using DG.Tweening;
using UnityEngine;

namespace Game.Scripts.Core.Animation
{
    [CreateAssetMenu(fileName = "Object Animation Data", menuName = "Doodle Jump/Object Animation Data")]
    public class ObjectAnimationData : ScriptableObject
    {
        [field: SerializeField] public float Delay { get; private set; }
        [field: SerializeField] public float Duration { get; private set; }
        [field: SerializeField] public Ease Ease { get; private set; }
    }
}