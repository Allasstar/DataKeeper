using System.Collections.Generic;
using UnityEngine;

namespace ActionStage.Core.Base
{
    public abstract class Container<TValue>
    {
        private Transform _root;
        private protected readonly Dictionary<string, TValue> _container = new Dictionary<string, TValue>();

        public int Count => _container.Keys.Count;
        public Dictionary<string, TValue> All => new (_container);

        
        public Transform Root()
        {
            if (_root == null)
            {
                _root = new GameObject($"[ActionStage Container]").transform;
                Object.DontDestroyOnLoad(_root);
            }

            return _root;
        }
        
        public virtual T Get<T>() where T : class
        {
            return _container[typeof(T).Name] as T;
        }
    
        public virtual T Get<T>(string id) where T : class
        {
            return _container[id] as T;
        }
    
        public virtual bool Contains<T>()
        {
            return _container.ContainsKey(typeof(T).Name);
        }
    
        public virtual bool Contains(string id)
        {
            return _container.ContainsKey(id);
        }
    
        public virtual void Remove<T>()
        {
            if (_container.ContainsKey(typeof(T).Name))
            {
                _container.Remove(typeof(T).Name);
            }
        }
        
        public virtual void Remove(string id)
        {
            if (_container.ContainsKey(id))
            {
                _container.Remove(id);
            }
        }
    }
}