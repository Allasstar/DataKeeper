using System;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Events;

[Serializable]
public class ReactivePref<T>
{
    [SerializeField]
    private T value;
    
    public T DefaultValue { get; private set; }
    
    public string Key { get; private set; }
    
    [NonSerialized]
    public UnityEvent<T> OnValueChanged = new UnityEvent<T>();
    
    [NonSerialized]
    public UnityEvent Event = new UnityEvent();
    
    public ReactivePref(T defaultValue, string key)
    {
        this.Key = key;
        switch (defaultValue)
        {
            case int i:
                value = (T)(object)PlayerPrefs.GetInt(key, i);
                break;
            case string s:
                value = (T)(object)PlayerPrefs.GetString(key, s);
                break;
            case float f:
                value = (T)(object)PlayerPrefs.GetFloat(key, f);
                break;
            case bool b:
                var res = PlayerPrefs.GetInt(key, b ? 1 : 0) == 1;
                value = (T)(object)res;
                break;
            default:
                var defaultJson = JsonConvert.SerializeObject(defaultValue);
                var json = PlayerPrefs.GetString(key, defaultJson);
                value = JsonConvert.DeserializeObject<T>(json);
                break;
        }
        
        DefaultValue = defaultValue;
    }

    public T Value
    {
        get => this.value;
       
        set
        {
            this.value = value;
            Save();
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

    public void Default()
    {
        value = DefaultValue;
    }
    
    public void Save()
    {
        switch (value)
        {
            case int i:
                PlayerPrefs.SetInt(Key, i);
                break;
            case string s:
                PlayerPrefs.SetString(Key, s);
                break;
            case float f:
                PlayerPrefs.SetFloat(Key, f);
                break;
            case bool b:
                PlayerPrefs.SetInt(Key, b ? 1 : 0);
                break;
            default:
                PlayerPrefs.SetString(Key, JsonConvert.SerializeObject(value));
                break;
        }
       
        PlayerPrefs.Save();
    }

    public void DefaultSave()
    {
        Default();
        Save();
    }
}