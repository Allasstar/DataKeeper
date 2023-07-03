# DataKeeper

## Reactive<T>

The `Reactive<T>` class is a generic class that represents a reactive variable of type `T`. It allows you to track changes to its value and invoke corresponding events when the value is modified.

### Usage

The `Reactive<T>` class can be used to create reactive variables that notify listeners when their value changes. It is commonly used in UI systems, game mechanics, and data binding scenarios.

```csharp
using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;

namespace DataKeeper.Generic
{
    [Serializable]
    public class Reactive<T> : IReactive
    {
        [SerializeField]
        private T value;
    
        [NonSerialized]
        public UnityEvent<T> OnValueChanged = new UnityEvent<T>();

        public static implicit operator T(Reactive<T> instance)
        {
            return instance.value;
        }
        
        // ... rest of the code ...

        public void Clear()
        {
            value = default(T);
        }
    }
    
    public interface IReactive
    {
        public void Invoke();
    }
}
