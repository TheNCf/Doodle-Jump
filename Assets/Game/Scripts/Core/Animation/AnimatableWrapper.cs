using System;
using UnityEngine;

namespace Game.Scripts.Core.Animation
{
    [Serializable]
    public struct AnimatableWrapper
    {
        [SerializeField] private UnityEngine.Object _targetObject;

        public IAnimatable Interface => _targetObject as IAnimatable;

        public void Validate()
        {
            if (_targetObject != null && !(_targetObject is IAnimatable))
            {
                Debug.LogError($"{_targetObject.name} didn't implement IAnimatable!");
                _targetObject = null;
            }
        }
    }
}