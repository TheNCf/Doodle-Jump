using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Game.Scripts.Core
{
    public class ObjectPool<T> where T : MonoBehaviour, IPoolableObject
    {
        private readonly HashSet<T> _pooledObjects;

        private int _pooledAtStart = 0;
        private int _countAll = 0;

        private Func<T> _createFunction;
        private Action<T> _onGetAction;
        private Action<T> _onReleaseAction;
        private Action<T> _onClearAction;

        public ObjectPool(Func<T> createFunction, Action<T> onGetAction, Action<T> onReleaseAction,
            Action<T> onClearAction,
            int pooledAtStart)
        {
            _createFunction = createFunction;
            _onGetAction = onGetAction;
            _onReleaseAction = onReleaseAction;
            _onClearAction = onClearAction;
            _pooledAtStart = pooledAtStart;

            _pooledObjects = new HashSet<T>();
            Initialize();
        }

        public int CountAll => _countAll;
        public int CountInactive => _pooledObjects.Count;
        public int CountActive => CountAll - CountInactive;

        public T Get()
        {
            T result = null;

            if (CountInactive > 0)
            {
                using var enumerator = _pooledObjects.GetEnumerator();
                
                if (enumerator.MoveNext())
                {
                    result = enumerator.Current;
                    _pooledObjects.Remove(result);
                }
            }

            if (result is null) 
            {
                result = _createFunction();
                _countAll++;
            }

            _onGetAction?.Invoke(result);
            return result;
        }

        public void Release(T obj)
        {
            _pooledObjects.Add(obj);
            _onReleaseAction?.Invoke(obj);
        }

        public void Clear()
        {
            if (_onClearAction != null)
                foreach (var obj in _pooledObjects)
                    _onClearAction?.Invoke(obj);

            _countAll = 0;
            _pooledObjects.Clear();
        }

        private void Initialize()
        {
            T buffer;

            for (int i = 0; i < _pooledAtStart; i++)
            {
                buffer = _createFunction();
                _countAll++;
                buffer.gameObject.name += $" ({_countAll})";
                _pooledObjects.Add(buffer);
            }
        }
    }
}