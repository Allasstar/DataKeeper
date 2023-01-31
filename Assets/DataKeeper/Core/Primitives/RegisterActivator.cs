using System;
using ActionStage.Core.Base;
using UnityEngine;

namespace ActionStage.Core.Primitives
{
    public class RegisterActivator<TValue> : Container<TValue>
    {
        public void Instantiate<T>(string id) where T : TValue
        {
            var instance = Activator.CreateInstance<T>();
            _container[id] = instance;
        }

        public void Instantiate<T>() where T : TValue
        {
            var instance = Activator.CreateInstance<T>();
            _container[typeof(T).Name] = instance;
        }

        public void InstantiateComponent<T>(string id, bool isDontDestroyOnLoad) where T : Component
        {
            var go = new GameObject($"[T: {typeof(T).Name}] [id: {id}]");
            var instance = go.AddComponent<T>();
            if (isDontDestroyOnLoad)
            {
                go.transform.parent = Root();
                // Object.DontDestroyOnLoad(instance);
            }
            _container[id] = (TValue)(object)instance;
        }
        
        public void InstantiateComponent<T>(bool isDontDestroyOnLoad) where T : Component
        {
            var id = typeof(T).Name;
            var go = new GameObject($"[T: {id}]");
            var instance = go.AddComponent<T>();
            if (isDontDestroyOnLoad)
            {
                go.transform.parent = Root();
                // Object.DontDestroyOnLoad(instance);
            }
            _container[id] = (TValue)(object)instance;
        }
    }
}