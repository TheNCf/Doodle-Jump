using Game.Scripts.Core.Animation;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    public class LoseMenuView : MonoBehaviour
    {
        [field: SerializeField] public AnimatableWrapper Animatable { get; private set; }
    }
}