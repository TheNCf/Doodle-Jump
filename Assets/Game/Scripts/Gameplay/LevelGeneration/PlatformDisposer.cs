using System;
using System.Collections.Generic;
using Game.Scripts.Core;

namespace Game.Scripts.Gameplay.LevelGeneration
{
    public class PlatformDisposer : IDisposable
    {
        private readonly CameraView _cameraView;
        private readonly List<IDisposableObject> _disposalBuffer = new();
        private readonly ObjectShifter _objectShifter;

        private readonly HashSet<IDisposableObject> _trackedObjects = new();

        public PlatformDisposer(ObjectShifter objectShifter, CameraView cameraView)
        {
            _objectShifter = objectShifter;
            _cameraView = cameraView;

            _objectShifter.RelativeHeightChanged += CheckForDisposal;
        }

        public void Dispose()
        {
            _objectShifter.RelativeHeightChanged -= CheckForDisposal;
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

            var disposalHeight = _objectShifter.RelativeHeight - _cameraView.Size.y / 2;

            foreach (var obj in _trackedObjects)
                if (obj.SpawnHeight < disposalHeight - obj.DistanceFromCenter)
                    _disposalBuffer.Add(obj);

            for (var i = _disposalBuffer.Count - 1; i >= 0; i--)
            {
                MarkedForDisposal?.Invoke(_disposalBuffer[i]);
                _trackedObjects.Remove(_disposalBuffer[i]);
            }

            _disposalBuffer.Clear();
        }
    }
}