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
    }
}