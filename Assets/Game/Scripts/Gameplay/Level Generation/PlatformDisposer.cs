using System;
using System.Collections.Generic;
using Game.Scripts.Core;

namespace Game.Scripts.Gameplay.LevelGeneration
{
    public class PlatformDisposer
    {
        private ObjectShifter _objectShifter;
        private CameraView _cameraView;
        
        private List<IDisposable> _trackedObjects = new List<IDisposable>();
        
        public PlatformDisposer(ObjectShifter objectShifter, CameraView cameraView)
        {
            _objectShifter = objectShifter;
            _cameraView = cameraView;

            _objectShifter.RelativeHeightChanged += CheckForDisposal;
        }

        public event Action<IDisposable> MarkedForDisposal;

        public void AddForTracking(IDisposable obj)
        {
            _trackedObjects.Add(obj);
        }

        public void RemoveFromTracking(IDisposable obj)
        {
            _trackedObjects.Remove(obj);
        }
        
        private void CheckForDisposal(float _)
        {
            if (_trackedObjects.Count == 0)
                return;
            
            for (int i = _trackedObjects.Count - 1; i >= 0; i--)
            {
                IDisposable obj = _trackedObjects[i];
                
                if (obj.SpawnHeight < _objectShifter.RelativeHeight - _cameraView.Size.y / 2 - obj.DistanceFromCenter)
                    MarkedForDisposal?.Invoke(obj);
            }
        }
    }
}