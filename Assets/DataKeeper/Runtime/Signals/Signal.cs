using System;
using System.Collections.Generic;

namespace DataKeeper.Signals
{
    public class Signal<T>
    {
        [field: NonSerialized] public List<Action<T>> Listeners { get; private set; } = new List<Action<T>>();
        
        public void Invoke(T value)
        {
            foreach (var listener in Listeners)
            {
                listener?.Invoke(value);
            }
        }

        public void AddListener(Action<T> listener)
        {
            Listeners.Add(listener);
        }

        public void RemoveListener(Action<T> listener)
        {
            Listeners.Remove(listener);
        }

        public void RemoveAllListeners()
        {
            Listeners.Clear();
        }
    }

    public class Signal
    {
        [field: NonSerialized] public List<Action> Listeners { get; private set; } = new List<Action>();
        
        public void Invoke()
        {
            foreach (var listener in Listeners)
            {
                listener?.Invoke();
            }
        }

        public void AddListener(Action listener)
        {
            Listeners.Add(listener);
        }

        public void RemoveListener(Action listener)
        {
            Listeners.Remove(listener);
        }

        public void RemoveAllListeners()
        {
            Listeners.Clear();
        }
    }
}

