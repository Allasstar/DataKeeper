using System;
using System.Collections.Generic;
using DataKeeper.Extra.ActCore;
using DataKeeper.Generic;
using UnityEngine;

public class TestAnumation : MonoBehaviour
{
    public DataFile<SomeData> SomeDataFileBin = new("Data/Bin/Some.bin", SerializationType.Binary);
    public DataFile<SomeData> SomeDataFileXml = new("Data/Xml/Some.xml", SerializationType.Xml);
    public DataFile<SomeData> SomeDataFileJson = new("Data/Json/Some.json", SerializationType.Json);

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
        var del = 0f;
        var damage = 30f;
        var dd = 0f;
        
        Act.Delta(2.5f, d =>
        {
            del += d;
            var dmg = damage * d;
            dd += dmg;
            Debug.Log($"del: {del} > d: {d}  dmg: {dmg}  >  damage: {damage} == dd: {dd}");
        });
        
        Act.DelayedCall(1f, Anim);
    }

    void Anim()
    {
        transform.position = Vector3.zero;
        
        Act.Float(0f, 20f, 1f, ActEase.InSin, i =>
        {
            var pos = Vector3.zero;
            pos.x = i;
            transform.position = pos;
            Debug.Log($"!!! Anim: {i}");
        }, () =>
        {
            Act.DelayedCall(3f, Anim);
        });

        // Act.Float(0f, 1f, 1f, i =>
        // {
        //     var pos = Vector3.zero;
        //     pos.x = ActEase.InOutSin(i, 0f, 20f);
        //     pos.z = ActEase.OutSin(i, 0f, 20f);
        //    
        //     Debug.Log($"!#$ Anim: {i}  Sin: {ActEase.Sin(i)}  Cos: {ActEase.Cos(i)}  Pos: {pos}");
        //
        //     transform.position = pos;
        //
        // }, () =>
        // {
        //     Act.DelayedCall(3f, Anim);
        // });
    }
    
    [Serializable]
    public class SomeData
    {
        public int Number = 236;
        public bool Boo = false;
        public float Flo = 0.0f;
        public string Str = "null";
        public Reactive<bool> React = new(true);
        public List<string> justList = new() {"qwer", "asdf", "zxcv", "qaz"};
        // public Dictionary<int, string> justDictionary = new() { {0, "000"}, {1, "111"}, {3, "333"} }; // Xml error
    }
}
