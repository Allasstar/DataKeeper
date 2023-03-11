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
        
        public static bool IsInsideCube(this Vector3 point, Vector3 cubePos, Vector3 cubeSize)
        {
            var min = cubePos - cubeSize / 2f;
            var max = cubePos + cubeSize / 2f;
            
            return point.x >= min.x 
                   && point.x <= max.x
                   && point.y >= min.y 
                   && point.y <= max.y
                   && point.z >= min.z 
                   && point.z <= max.z;
        }
        
        public static bool IsInsideCube(this Transform tr, Vector3 cubePos, Vector3 cubeSize)
        {
            return tr.position.IsInsideCube(cubePos, cubeSize);
        }
        
        public static bool IsInsideSphere(this Vector3 point, Vector3 spherePos, float sphereRadius)
        {
            return Vector3.Distance(point, spherePos) <= sphereRadius;
        }
        
        public static bool IsInsideSphere(this Transform tr, Vector3 spherePos, float sphereRadius)
        {
            return Vector3.Distance(tr.position, spherePos) <= sphereRadius;
        }
    }
}
