using System;
using System.Collections.Generic;
using System.Linq;
using Game.Scripts.Core;

namespace Game.Scripts.Gameplay.LevelGeneration
{
    public class PlatformDisposer
    {
        private ObjectShifter _objectShifter;
        private CameraView _cameraView;

        private HashSet<IDisposableObject> _trackedObjects = new();
        private readonly List<IDisposableObject> _disposalBuffer = new();

        public PlatformDisposer(ObjectShifter objectShifter, CameraView cameraView)
        {
            _objectShifter = objectShifter;
            _cameraView = cameraView;

            _objectShifter.RelativeHeightChanged += CheckForDisposal;
        }

        public event Action<IDisposableObject> MarkedForDisposal;

        public void AddForTracking(IDisposableObject obj)
        {
            _trackedObjects.Add(obj);
        }

        public void RemoveFromTracking(IDisposableObject obj)
        {
            _trackedObjects.Remove(obj);
        }

        private void CheckForDisposal(float _)
        {
            if (_trackedObjects.Count == 0)
                return;

            float disposalHeight = _objectShifter.RelativeHeight - _cameraView.Size.y / 2;
            
            foreach (IDisposableObject obj in _trackedObjects)
                if (obj.SpawnHeight < disposalHeight - obj.DistanceFromCenter)
                    _disposalBuffer.Add(obj);
            
            for (int i = _disposalBuffer.Count - 1; i >= 0; i--)
            {
                MarkedForDisposal?.Invoke(_disposalBuffer[i]);
                _trackedObjects.Remove(_disposalBuffer[i]);
            }
            
            _disposalBuffer.Clear();
        }
    }
}