using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DataKeeper.Generic.Base
{
    public abstract class Container<TValue> where TValue : class
    {
        private static Transform _root;
        protected readonly Dictionary<string, TValue> _container = new Dictionary<string, TValue>();

        public int Count => _container.Count;
        public IReadOnlyDictionary<string, TValue> All => _container;

        public void ClearNull()
        {
            var keysToRemove = _container.Where(kvp => kvp.Value == null).Select(kvp => kvp.Key).ToList();
            foreach (var key in keysToRemove)
            {
                _container.Remove(key);
            }
        }

        protected static Transform Root
        {
            get
            {
                if (_root == null)
                {
                    _root = new GameObject("[DataKeeper Container]").transform;
                    Object.DontDestroyOnLoad(_root);
                }
                return _root;
            }
        }
        
        public virtual T Get<T>() where T : class, TValue
        {
            return _container.TryGetValue(typeof(T).Name, out var value) ? value as T : null;
        }
    
        public virtual T Get<T>(string id) where T : class, TValue
        {
            return _container.TryGetValue(id, out var value) ? value as T : null;
        }
    
        public virtual bool Contains<T>() => _container.ContainsKey(typeof(T).Name);
    
        public virtual bool Contains(string id) => _container.ContainsKey(id);
    
        public virtual bool Remove<T>() => _container.Remove(typeof(T).Name);
        
        public virtual bool Remove(string id) => _container.Remove(id);
    }
}