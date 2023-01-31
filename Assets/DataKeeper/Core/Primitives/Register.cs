using DataKeeper.Core.Base;

namespace DataKeeper.Core.Primitives
{
    public class Register<TValue> : Container<TValue>
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
