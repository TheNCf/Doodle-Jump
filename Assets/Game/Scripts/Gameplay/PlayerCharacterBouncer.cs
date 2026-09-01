using UnityEngine;
using Zenject;

namespace Game.Scripts.Gameplay
{
    public class PlayerCharacterBouncer : IFixedTickable
    {
        private readonly PlayerCharacterView _playerCharacterView;
        
        private readonly Rigidbody2D _rigidbody;
        private readonly Collider2D _legCollider;

        public PlayerCharacterBouncer(PlayerCharacterView playerCharacterView)
        {
            _playerCharacterView = playerCharacterView;
            
            _rigidbody = _playerCharacterView.Rigidbody;
            _legCollider = _playerCharacterView.LegCollider;

            _playerCharacterView.LegsColliding += Bounce;
        }

        public void FixedTick()
        {
            ToggleLegs();
        }

        private void Bounce(Collision2D collision)
        {
            if (collision.collider.TryGetComponent(out IBounceable bounceable) == false)
                return;
                
            Vector2 newVelocity = _rigidbody.velocity;
            newVelocity.y = bounceable.BounceMultiplier * _playerCharacterView.JumpStrength;
            _rigidbody.velocity = newVelocity;
        }
        
        private void ToggleLegs()
        {
            _legCollider.enabled = _rigidbody.velocity.y < 0;
        }
    }
}