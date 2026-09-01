using UnityEngine;

namespace Game.Scripts.Gameplay
{
    [CreateAssetMenu(fileName = "Bounce Config", menuName = "Doodle Jump/Bounce Config")]
    public class BounceConfig : ScriptableObject
    {
        [field: SerializeField] public BounceType Type { get; private set; }
        [field: SerializeField] public Sprite Sprite { get; private set; }
        [field: SerializeField] public Sprite ActivatedSprite { get; private set; }
        [field: SerializeField] public float BounceMultiplier { get; private set; }
        [field: SerializeField] public float Speed { get; private set; }
        [field: SerializeField] public float FallSpeed { get; private set; }
        [field: SerializeField] public bool IsOneTimeUse { get; private set; }
    }
}