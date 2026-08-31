using System;
using System.Collections.Generic;
using Game.Scripts.Core;
using UnityEngine;

namespace Game.Scripts.Gameplay
{
    public class PlatformDisposer
    {
        private ObjectShifter _objectShifter;
        private CameraView _cameraView;
        
        private List<BounceView> _trackedPlatforms = new List<BounceView>();
        
        public PlatformDisposer(ObjectShifter objectShifter, CameraView cameraView)
        {
            _objectShifter = objectShifter;
            _cameraView = cameraView;

            _objectShifter.RelativeHeightChanged += CheckForDisposal;
        }

        public event Action<BounceView> MarkedForDisposal;

        public void AddForTracking(BounceView platform)
        {
            _trackedPlatforms.Add(platform);
        }

        public void RemoveFromTracking(BounceView platform)
        {
            _trackedPlatforms.Remove(platform);
        }
        
        private void CheckForDisposal(float _)
        {
            if (_trackedPlatforms.Count == 0)
                return;
            
            for (int i = _trackedPlatforms.Count - 1; i >= 0; i--)
            {
                BounceView platform = _trackedPlatforms[i];
                
                if (platform.SpawnHeight < _objectShifter.RelativeHeight - _cameraView.Size.y / 2 - platform.DistanceFromCenter)
                    MarkedForDisposal?.Invoke(platform);
            }
        }
    }
}