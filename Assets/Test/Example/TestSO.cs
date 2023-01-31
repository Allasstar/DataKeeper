using DataKeeper.Base;
using DataKeeper.Extra.ActCore;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Test.Example
{
    [CreateAssetMenu(fileName = "SO", menuName = "Test ActionStage/SO 1", order = 1)]
    public class TestSO : SO
    {
        public string id;
        public override void Initialize()
        {
            Debug.Log($"Initialize > TestSO: {id}");
            DK.SO.Reg(this, id);
            
            DK.Any.InstantiateComponent<DontDesClass>(true);
            DK.Any.InstantiateComponent<DontDesClass>("Num2", true);
            DK.Any.InstantiateComponent<SceneClass>(false);
            DK.Any.Instantiate<SimpleClass>();
            
            Act.DelayedCall(10f, () => SceneManager.LoadScene(1));
        }
    }
}
