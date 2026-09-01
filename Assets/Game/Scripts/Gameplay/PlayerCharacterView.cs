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
        [SerializeField] private float _heightToShift = 1.0f;

        public event Action<Collision2D> LegsColliding;
    
        public Rigidbody2D Rigidbody => _rigidbody;
        public Transform Transform => _transform;
        public SpriteRenderer SpriteRenderer => _spriteRenderer;
        public Collider2D LegCollider => _legCollider;
        
        public float MaxSpeed => _maxSpeed;
        public float SpeedInterpolation => _speedInterpolation;
        
        public float JumpStrength => _jumpStrength;
        public float HeightToShift => _heightToShift;

        private void OnCollisionEnter2D(Collision2D other)
        {
            LegsColliding?.Invoke(other);
        }

        public float GetJumpHeight()
        {
            float gravity = Mathf.Abs(Physics2D.gravity.y);
            float effectiveGravity = gravity * _rigidbody.gravityScale; 
            float height = (_jumpStrength * _jumpStrength) / (2.0f * effectiveGravity);
            return height;
        }
    }
}