using UnityEngine;

namespace DataKeeper.Extensions
{
    public static class Vector3Extension
    {
        public static void SetPosX(this Transform tr, float x)
        {
            var v = tr.position;
            v.x = x;
            tr.position = v;
        }
    
        public static void SetPosY(this Transform tr, float y)
        {
            var v = tr.position;
            v.y = y;
            tr.position = v;
        }
    
        public static void SetPosZ(this Transform tr, float z)
        {
            var v = tr.position;
            v.z = z;
            tr.position = v;
        }
    
        public static void SetLocalPosX(this Transform tr, float x)
        {
            var v = tr.localPosition;
            v.x = x;
            tr.localPosition = v;
        }
    
        public static void SetLocalPosY(this Transform tr, float y)
        {
            var v = tr.localPosition;
            v.y = y;
            tr.localPosition = v;
        }
    
        public static void SetLocalPosZ(this Transform tr, float z)
        {
            var v = tr.localPosition;
            v.z = z;
            tr.localPosition = v;
        }
    }
}
