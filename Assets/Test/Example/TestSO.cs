using System;
using System.Collections.Generic;
using DataKeeper.Attributes;
using DataKeeper.Base;
using DataKeeper.Extra.ActCore;
using DataKeeper.Generic;
using Newtonsoft.Json;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.SceneManagement;

namespace Test.Example
{
    [CreateAssetMenu(fileName = "SO", menuName = "Test ActionStage/SO 1", order = 1)]
    public class TestSO : SO
    {
        public TestJson testJson;
        public string id;

        [ReadOnlyInspector] public int iId = 5;
        public int iId2 = 5;

        [ReadOnlyInspector] public List<int> testInt = new() { 3,7,10};

        public string sid;
        public bool bid;


        public override void Initialize()
        {
            Debug.Log($"Initialize > TestSO: {id}");
            DK.SO.Reg(this, id);
            
            DK.Any.InstantiateComponent<DontDesClass>(true);
            DK.Any.InstantiateComponent<DontDesClass>("Num2", true);
            DK.Any.InstantiateComponent<SceneClass>(false);
            DK.Any.Instantiate<SimpleClass>();
            
            Act.DelayedCall(10f, () => SceneManager.LoadScene(1));
            
            Debug.Log(testJson.ToJSON(Formatting.Indented));
        }
    }

    [Serializable]
    public class TestJson: JsonData<TestJson>
    {
        public ReactiveList<int> tesRL = new ();

    }
}
