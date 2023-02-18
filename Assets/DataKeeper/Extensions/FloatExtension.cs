using UnityEngine;

public static class FloatExtension
{
    public static float Remap(this float value, float fromMin, float fromMax, float toMin,  float toMax)
    {
        return Mathf.Lerp(toMin, toMax, Mathf.InverseLerp(fromMin, fromMax, value));
    }
}
