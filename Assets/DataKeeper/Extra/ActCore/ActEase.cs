using DataKeeper.Extra.ActCore;
using UnityEngine;

public class ActEase
{
   // --- Linear ---
   /// <param name="value">From 0 to 1.</param>
   /// <param name="from">Any float.</param>
   /// <param name="to">Any float.</param>
   /// <returns>Ease (float)</returns>
   public static Ease Linear(float value, float from = 0f, float to = 1f)
   {
      return value.Map(from, to);
   }
   
   // --- Sin ---
   public static Ease Sin(float value, float from = 0f, float to = 1f)
   {
      return Mathf.Sin(value).Remap(-1f, 1f, from, to);
   }
   
   /// <param name="value">From 0 to 1.</param>
   /// <param name="from">Any float.</param>
   /// <param name="to">Any float.</param>
   /// <returns>Ease (float)</returns>
   public static Ease InSin(float value, float from = 0f, float to = 1f)
   {
      return (1f - Mathf.Cos((value * Mathf.PI) / 2f)).Map(from, to);
   }
   
   /// <param name="value">From 0 to 1.</param>
   /// <param name="from">Any float.</param>
   /// <param name="to">Any float.</param>
   /// <returns>Ease (float)</returns>
   public static Ease OutSin(float value, float from = 0f, float to = 1f)
   {
      return Mathf.Sin((value * Mathf.PI) / 2f).Map(from, to);
   }
   
   /// <param name="value">From 0 to 1.</param>
   /// <param name="from">Any float.</param>
   /// <param name="to">Any float.</param>
   /// <returns>Ease (float)</returns>
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
