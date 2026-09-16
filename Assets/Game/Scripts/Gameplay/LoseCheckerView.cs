using System;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    public class LoseCheckerView : MonoBehaviour
    {
        private Vector3 _gizmosExtends = new (5.0f, 0, 0);
        
        public Action Lose;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(transform.position + _gizmosExtends, transform.position - _gizmosExtends);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.GetComponent<PlayerCharacterView>())
                Lose?.Invoke();
            
            if (other.GetComponentInParent<PlayerCharacterView>())
                Lose?.Invoke();
        }
    }
}