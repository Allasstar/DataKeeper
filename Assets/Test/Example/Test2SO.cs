using ActionStage.Core.Base;
using UnityEngine;

namespace ActionStage
{
    [CreateAssetMenu(fileName = "SO", menuName = "Test ActionStage/SO 2", order = 1)]
    public class Test2SO : SOBase
    {
        public string id;
        public override void Initialize()
        {
            Debug.Log($"Initialize > Test2SO: {id}");
            Model.SO.Reg(this, id);
        }
    }
}
