using DataKeeper.Base;
using UnityEngine;

namespace Test.Example
{
    [CreateAssetMenu(fileName = "SO", menuName = "Test ActionStage/SO 2", order = 1)]
    public class Test2SO : SO
    {
        public string id;
        
        [field: SerializeField] public TestInstPrefabFromSO Prefab { get; private set; }  
        
        
        public override void Initialize()
        {
            Debug.Log($"Initialize > Test2SO: {id}");
            DK.SO.Reg(this, id);

            DontDestroyOnLoad(Instantiate(Prefab));
            Instantiate(Prefab).gameObject.name = "SimpleSample Prefab";
        }
    }
}
