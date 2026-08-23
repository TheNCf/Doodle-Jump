using UnityEngine;

namespace Game.Scripts.Core
{
    public class CameraView : MonoBehaviour
    {
        [SerializeField] private Camera _camera;
        
        public Transform Transform => _camera.transform;

        public Vector2 Size => _camera.ScreenToWorldPoint(new Vector3(Screen.width, Screen.height));
    }
}