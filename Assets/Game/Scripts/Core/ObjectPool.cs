using System;
using System.Collections.Generic;
using UnityEngine;

namespace Game.Scripts.Core
{
    public class ObjectPool<T> where T : MonoBehaviour, IPoolableObject
    {
        private readonly Func<T> _createFunction;
        private readonly Action<T> _onClearAction;
        private readonly Action<T> _onGetAction;
        private readonly Action<T> _onReleaseAction;

        private readonly int _pooledAtStart;
        private readonly HashSet<T> _pooledObjects;

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

        public int CountAll { get; private set; }

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
                CountAll++;
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

            CountAll = 0;
            _pooledObjects.Clear();
        }

        private void Initialize()
        {
            T buffer;

            for (var i = 0; i < _pooledAtStart; i++)
            {
                buffer = _createFunction();
                CountAll++;
                buffer.gameObject.name += $" ({CountAll})";
                _pooledObjects.Add(buffer);
            }
        }
    }
}