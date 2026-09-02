using System;
using UnityEngine;
using Zenject;

namespace Game.Scripts.Core
{
    public class ObjectPoolFactory
    {
        public static ObjectPool<T> CreateMonoPool<T>(DiContainer container, T prefab, int initialSize, Action<T> onGet = null, Action<T> onRelease = null) where T : MonoBehaviour, IPoolableObject
        {
            T Create()
            {
                T spawnedObject = container.InstantiatePrefabForComponent<T>(prefab);
                spawnedObject.gameObject.SetActive(false);
                return spawnedObject;
            }

            void OnGet(T item)
            {
                onGet?.Invoke(item);
                item.gameObject.SetActive(true);
            }

            void OnRelease(T item)
            {
                onRelease?.Invoke(item);
                item.gameObject.SetActive(false);
            }

            void OnClear(T item)
            {
                if (item != null && item.gameObject != null) 
                    UnityEngine.Object.Destroy(item.gameObject);
            }

            return new ObjectPool<T>(Create, OnGet, OnRelease, OnClear, initialSize);
        }
    }
}