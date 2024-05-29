using UnityEngine;

namespace DataKeeper.ActCore
{
   public class ActEase
   {
      // --- Linear ---
      /// <param name="value">From 0 to 1.</param>
      /// <param name="from">Any float.</param>
      /// <param name="to">Any float.</param>
      /// <returns>FloatEase (float)</returns>
      public static FloatEase Linear(float value, float from = 0f, float to = 1f)
      {
         return value.Map(from, to);
      }
   
      // --- Sin ---
      public static FloatEase Sin(float value, float from = 0f, float to = 1f)
      {
         return Mathf.Sin(value).Remap(-1f, 1f, from, to);
      }
   
      /// <param name="value">From 0 to 1.</param>
      /// <param name="from">Any float.</param>
      /// <param name="to">Any float.</param>
      /// <returns>FloatEase (float)</returns>
      public static FloatEase InSin(float value, float from = 0f, float to = 1f)
      {
         return (1f - Mathf.Cos((value * Mathf.PI) / 2f)).Map(from, to);
      }
   
      /// <param name="value">From 0 to 1.</param>
      /// <param name="from">Any float.</param>
      /// <param name="to">Any float.</param>
      /// <returns>FloatEase (float)</returns>
      public static FloatEase OutSin(float value, float from = 0f, float to = 1f)
      {
         return Mathf.Sin((value * Mathf.PI) / 2f).Map(from, to);
      }
   
      /// <param name="value">From 0 to 1.</param>
      /// <param name="from">Any float.</param>
      /// <param name="to">Any float.</param>
      /// <returns>FloatEase (float)</returns>
      public static FloatEase InOutSin(float value, float from = 0f, float to = 1f)
      {
         return ((Mathf.Cos(Mathf.PI * value) - 1) / 2).Map(from, to);
      }
   
      // --- Cos ---
      public static FloatEase Cos(float value, float from = 0f, float to = 1f)
      {
         return Mathf.Cos(value).Remap(-1f, 1f, from, to);
      }
   }
}
