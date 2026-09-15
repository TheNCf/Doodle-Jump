using System;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    public class LoseCheckerView : MonoBehaviour
    {
        private Vector3 _gizmosExtends = new Vector3(5.0f, 0, 0);

        public Transform Transform => transform;

        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            Gizmos.DrawLine(Transform.position + _gizmosExtends, Transform.position - _gizmosExtends);
        }
    }
}