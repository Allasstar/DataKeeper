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
        
        public Reactive()
        {
            this.value = default(T);
        }

        public Reactive(T value)
        {
            this.value = value;
        }

        public T Value
        {
            get => this.value;
       
            set
            {
                this.value = value;
                this.OnValueChanged?.Invoke(value);
            }
        }
    
        [JsonIgnore]
        public T SilentValue
        {
            get => this.value;
            set => this.value = value;
        }
        
        public void Invoke()
        {
            this.OnValueChanged?.Invoke(value);
        }

        public void SilentChange(T value)
        {
            this.value = value;
        }

        public void AddListener(UnityAction<T> call)
        {
            OnValueChanged.AddListener(call);
        }
    
        public void RemoveListener(UnityAction<T> call)
        {
            OnValueChanged.RemoveListener(call);
        }

        public void RemoveAllListeners()
        {
            OnValueChanged.RemoveAllListeners();
        }
    
        public override string ToString()
        {
            return value.ToString();
        }

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