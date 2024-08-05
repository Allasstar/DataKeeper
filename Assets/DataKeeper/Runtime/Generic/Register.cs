using DataKeeper.Generic.Base;

namespace DataKeeper.Generic
{
    public class Register<TValue> : Container<TValue> where TValue : class
    {
        public void Reg(TValue value, string id)
        {
            _container[id] = value;
        }
    
        public void Reg<T>(TValue value) where T : TValue
        {
            _container[typeof(T).Name] = value;
        }
        
        public void Reg(TValue value)
        {
            _container[typeof(TValue).Name] = value;
        }
        
        public T Get<T>() where T : class, TValue
        {
            return _container.TryGetValue(typeof(T).Name, out var value) ? value as T : null;
        }
    
        public T Get<T>(string id) where T : class, TValue
        {
            return _container.TryGetValue(id, out var value) ? value as T : null;
        }
        
        public Register<TValue> Get<T>(out T outValue) where T : class, TValue
        {
            outValue = _container.TryGetValue(typeof(T).Name, out var value) ? value as T : null;
            return this;
        }
        
        public Register<TValue> Get<T>(string id, out T outValue) where T : class, TValue
        {
            outValue = _container.TryGetValue(id, out var value) ? value as T : null;
            return this;
        }
        
        public bool TryGet<T>(out T outValue) where T : class, TValue
        {
            outValue = _container.TryGetValue(typeof(T).Name, out var value) ? value as T : null;
            return outValue != null;
        }
        
        public bool TryGet<T>(string id, out T outValue) where T : class, TValue
        {
            outValue = _container.TryGetValue(id, out var value) ? value as T : null;
            return outValue != null;
        }
        
        public bool Remove<T>() => _container.Remove(typeof(T).Name);
        
        public bool Remove(string id) => _container.Remove(id);
    }
}