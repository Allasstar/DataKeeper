using DataKeeper.Core.ActEngine;
using DataKeeper.Core.Base;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Test.Example
{
    [CreateAssetMenu(fileName = "SO", menuName = "Test ActionStage/SO 1", order = 1)]
    public class TestSO : SOBase
    {
        public string id;
        public override void Initialize()
        {
            Debug.Log($"Initialize > TestSO: {id}");
            Model.SO.Reg(this, id);
            
            Model.Any.InstantiateComponent<DontDesClass>(true);
            Model.Any.InstantiateComponent<DontDesClass>("Num2", true);
            Model.Any.InstantiateComponent<SceneClass>(false);
            Model.Any.Instantiate<SimpleClass>();
            
            Act.DelayedCall(10f, () => SceneManager.LoadScene(1));
        }
    }
}
