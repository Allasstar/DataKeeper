using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;

namespace ActionStage.Core.Primitives
{
    [Serializable]
    public class Reactive<T>
    {
        [SerializeField]
        private T value;
    
        [NonSerialized]
        public UnityEvent<T> OnValueChanged = new UnityEvent<T>();
    
        [NonSerialized]
        public UnityEvent Event = new UnityEvent();
    
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

        public void SilentChange(T value)
        {
            try
            {
                this.value = value;
            }
            catch (Exception e)
            {
                Debug.Log($"Reactive Exception: {e.Message}");
            }
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
}