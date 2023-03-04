using DataKeeper.Extra;
using UnityEngine;

namespace Test.Example
{
    public class MaterialTest : MonoBehaviour
    {
        private MeshRenderer _meshRenderer;
        private MaterialAction _materialAction;
        
        private void Awake()
        {
            _meshRenderer = GetComponent<MeshRenderer>();
            _materialAction = new MaterialAction(_meshRenderer);
        }

        private void Update()
        {
            _materialAction.Act(p =>
            {
                Color color = new Color(1,1,1,1);
            
                color.r = (Mathf.Sin(transform.position.x * Time.time) + 2) / 2f;
                color.g = (Mathf.Sin(transform.position.y * Time.time * 2) + 2) / 2f;
                color.b = (Mathf.Sin(transform.position.z * Time.time * 0.5f) + 2) / 2f;
                
                p.SetColor("_BaseColor", color);
            });
        }
    }
}
