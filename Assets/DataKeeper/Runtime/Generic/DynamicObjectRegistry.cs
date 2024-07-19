using System;
using DataKeeper.Generic.Base;
using UnityEngine;

namespace DataKeeper.Generic
{
    public class DynamicObjectRegistry<TValue> : Container<TValue> where TValue : class
    {
        public T GetOrInstantiate<T>(string id) where T : TValue, new()
        {
            if (_container.TryGetValue(id, out TValue value) && value is T typedValue)
            {
                return typedValue;
            }
            return Instantiate<T>(id);
        }

        public T GetOrInstantiate<T>() where T : TValue, new()
        {
            return GetOrInstantiate<T>(typeof(T).Name);
        }

        public T GetOrInstantiateComponent<T>(string id, bool isDontDestroyOnLoad = false) where T : Component
        {
            if (_container.TryGetValue(id, out TValue value) && value is T typedValue)
            {
                return typedValue;
            }
            return InstantiateComponent<T>(id, isDontDestroyOnLoad);
        }

        public T GetOrInstantiateComponent<T>(bool isDontDestroyOnLoad = false) where T : Component
        {
            return GetOrInstantiateComponent<T>(typeof(T).Name, isDontDestroyOnLoad);
        }

        public T Instantiate<T>(string id) where T : TValue, new()
        {
            var instance = new T();
            _container[id] = instance;
            return instance;
        }

        public T Instantiate<T>() where T : TValue, new()
        {
            return Instantiate<T>(typeof(T).Name);
        }

        public T InstantiateComponent<T>(string id, bool isDontDestroyOnLoad = false) where T : Component
        {
            var go = new GameObject($"[T: {typeof(T).Name}] [id: {id}]");
            var instance = go.AddComponent<T>();
            if (isDontDestroyOnLoad)
            {
                go.transform.SetParent(Root, worldPositionStays: false);
            }
            _container[id] = instance as TValue ?? throw new InvalidCastException($"Cannot cast {typeof(T).Name} to {typeof(TValue).Name}");
            return instance;
        }
        
        public T InstantiateComponent<T>(bool isDontDestroyOnLoad = false) where T : Component
        {
            return InstantiateComponent<T>(typeof(T).Name, isDontDestroyOnLoad);
        }
    }
}