using DataKeeper.Extra.ActCore;
using UnityEngine;

public class TestAnumation : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
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

}