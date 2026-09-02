using UnityEngine;

namespace Game.Scripts.Core
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        public Transform Transform => _camera.transform;

        public Vector2 Size => new Vector3(_camera.aspect, 1) * _camera.orthographicSize * 2f;
    }
}