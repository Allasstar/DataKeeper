using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DataKeeper.Generic.Base
{
    public abstract class Container<TValue>
    {
        private static Transform _root;
        private protected readonly Dictionary<string, TValue> _container = new Dictionary<string, TValue>();

        public int Count => _container.Keys.Count;
        public Dictionary<string, TValue> All => new (_container);

        public void ClearNull()
        {
            var selected = _container.Where(w => w.Value == null)
                .Select(s => s.Key);

            foreach (var key in selected)
            {
                Remove(key);
            }
        }

        protected Transform Root()
        {
            if (_root == null)
            {
                _root = new GameObject($"[DataKeeper Container]").transform;
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