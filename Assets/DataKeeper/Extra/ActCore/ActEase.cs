using DataKeeper.Extra.ActCore;
using UnityEngine;

public class ActEase
{
   // --- Linear ---
   public static Ease Linear(float value, float from = 0f, float to = 1f)
   {
      return value.Map(from, to);
   }
   
   // --- Sin ---
   public static Ease Sin(float value, float from = 0f, float to = 1f)
   {
      return Mathf.Sin(value).Remap(-1f, 1f, from, to);
   }
   
   public static Ease InSin(float value, float from = 0f, float to = 1f)
   {
      return (1f - Mathf.Cos((value * Mathf.PI) / 2f)).Map(from, to);
   }
   
   public static Ease OutSin(float value, float from = 0f, float to = 1f)
   {
      return Mathf.Sin((value * Mathf.PI) / 2f).Map(from, to);
   }
   
   public static Ease InOutSin(float value, float from = 0f, float to = 1f)
   {
      return ((Mathf.Cos(Mathf.PI * value) - 1) / 2).Map(from, to);
   }
   
   // --- Cos ---
   public static Ease Cos(float value, float from = 0f, float to = 1f)
   {
      return Mathf.Cos(value).Remap(-1f, 1f, from, to);
   }
}
