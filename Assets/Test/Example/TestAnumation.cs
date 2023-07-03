using System;
using System.Collections.Generic;
using DataKeeper.Generic;
using UnityEngine;

public class TestAnumation : MonoBehaviour
{
    public Color _color1;
    public Color _color2;
    
    public DataFile<SomeData> SomeDataFileBin = new("Data/Bin/Some.bin", SerializationType.Binary);
    public DataFile<SomeData> SomeDataFileXml = new("Data/Xml/Some.xml", SerializationType.Xml);
    public DataFile<SomeData> SomeDataFileJson = new("Data/Json/Some.json", SerializationType.Json);
    
    public ReactivePref<SomeData> SomeDataPref = new(new SomeData(), "some_data");
    public ReactivePref<bool> BoolPref = new(false, "some_data_bool");
    
    public Reactive<SomeData> SomeDataReact = new();
    public Reactive<bool> BoolReact = new();
    public Reactive<string> StringReact = new();

    [ContextMenu("Open")]
    private void Open()
    {
        Application.OpenURL(Application.persistentDataPath);
    }
    
    [ContextMenu("Save")]
    private void Save()
    {
        SomeDataFileBin.SaveData();
        SomeDataFileXml.SaveData();
        SomeDataFileJson.SaveData();
    }

    [ContextMenu("Load")]
    private void Load()
    {
        SomeDataFileBin.LoadData();
        SomeDataFileXml.LoadData();
        SomeDataFileJson.LoadData();
    }


    // Start is called before the first frame update
    void Start()
    {
        BoolReact.AddListener(BoolReactChanged);
    }

    private void BoolReactChanged(bool value)
    {
        Debug.Log($"BoolReactChanged: {value}");
    }

    [Serializable]
    public class SomeData
    {
        public int Number = 236;
        public bool Boo = false;
        public float Flo = 0.0f;
        public string Str = "null";
        public List<string> justList = new() {"qwer", "asdf", "zxcv", "qaz"};
        // public Dictionary<int, string> justDictionary = new() { {0, "000"}, {1, "111"}, {3, "333"} }; // Xml error
    }
}
