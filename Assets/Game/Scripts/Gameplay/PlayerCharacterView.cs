using UnityEngine;

namespace Game.Scripts.Gameplay
{
    public class PlayerCharacterView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _transform;
        [SerializeField] private SpriteRenderer _spriteRenderer;
    
        [SerializeField] private float _maxSpeed = 5.0f;
        [SerializeField] private float _speedInterpolation = 20.0f;
    
        public Rigidbody2D Rigidbody => _rigidbody;
        public Transform Transform => _transform;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        
        public float MaxSpeed => _maxSpeed;
        public float SpeedInterpolation => _speedInterpolation;
    }
}