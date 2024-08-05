using System.Collections.Generic;
using System.Linq;

namespace DataKeeper.Generic.Base
{
    public abstract class Container<TValue> where TValue : class
    {
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
    
        public virtual bool Contains<T>() => _container.ContainsKey(typeof(T).Name);
    
        public virtual bool Contains(string id) => _container.ContainsKey(id);
    }
}