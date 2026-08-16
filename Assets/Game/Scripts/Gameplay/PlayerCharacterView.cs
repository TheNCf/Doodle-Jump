using System;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    public class PlayerCharacterView : MonoBehaviour
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Transform _transform;
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Collider2D _legCollider;
    
        [SerializeField] private float _maxSpeed = 5.0f;
        [SerializeField] private float _speedInterpolation = 20.0f;

        [SerializeField] private float _jumpStrength = 8.0f;

        public event Action<Collision2D> LegsOverlapping; 
    
        public Rigidbody2D Rigidbody => _rigidbody;
        public Transform Transform => _transform;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Collider2D LegCollider => _legCollider;
        
        public float MaxSpeed => _maxSpeed;
        public float SpeedInterpolation => _speedInterpolation;
        
        public float JumpStrength => _jumpStrength;

        private void OnCollisionEnter2D(Collision2D other)
        {
            LegsOverlapping?.Invoke(other);
        }
    }
}