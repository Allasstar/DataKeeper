using System;
using ActionStage.Core.Primitives;
using UnityEngine;

namespace ActionStage
{
    public class TestAutoRun : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;
        private MaterialAction _materialAction;
        
        private void Awake()
        {
            // var qwe = Model.SO.Get<Test2SO>("qwe");
            // var qaz = Model.SO.Get<TestSO>("qaz");
            // var zxc = Model.SO.Get<TestSO>("zxc");
            // var zaq = Model.SO.Get<Test2SO>("zaq");
            //
            // Debug.Log($"qwe: {qwe.id}");
            // Debug.Log($"qaz: {qaz.id}");
            // Debug.Log($"zxc: {zxc.id}");
            // Debug.Log($"zaq: {zaq.id}");

            _meshRenderer = GetComponent<MeshRenderer>();
            _materialAction = new MaterialAction(_meshRenderer);
        }

        private void Update()
        {
            _materialAction.Act(p =>
            {
                Color color = new Color(1,1,1,1);
            
                color.r = (Mathf.Sin(transform.position.x * Time.time) + 2) / 2f;
                color.g = (Mathf.Sin(transform.position.y * Time.time) + 2) / 2f;
                color.b = (Mathf.Sin(transform.position.z * Time.time) + 2) / 2f;
                
                p.SetColor("_BaseColor", color);
            });
        }
    }
}
