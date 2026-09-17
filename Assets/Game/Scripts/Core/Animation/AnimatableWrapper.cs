using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Game.Scripts.Core.Animation
{
    [Serializable]
    public struct AnimatableWrapper
    {
        [SerializeField] private Object _targetObject;

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