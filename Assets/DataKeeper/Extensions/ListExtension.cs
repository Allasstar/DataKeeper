using System;
using System.Collections.Generic;
using System.Linq;
using Random = System.Random;

namespace DataKeeper.Extensions
{
   public static class ListExtension
   {
      public static void Swap<T>(this List<T> list, int indexA, int indexB)
      {
         (list[indexA], list[indexB]) = (list[indexB], list[indexA]);
      }
   
      public static T Pop<T>(this IList<T> list)
      {
         if(list.Count == 0)
            return default;
      
         var item = list[0];
         list.RemoveAt(0);
         return item;
      }
   
      public static T PopLast<T>(this IList<T> list)
      {
         if(list.Count == 0)
            return default;
      
         var item = list[^1];
         list.Remove(item);
         return item;
      }
   
      public static T Random<T>(this IList<T> list)
      {
         return list[UnityEngine.Random.Range(0, list.Count)];
      }
   
      public static T RandomSystem<T>(this IList<T> list)
      {
         Random rnd = new Random();
         return list[rnd.Next(0, list.Count - 1)];
      }
   
      public static bool HasIndex<T>(this IList<T> list, int index)
      {
         if (index < 0) return false;
         return list.Count > index;
      }
   
      public static T Get<T>(this IList<T> list, int index)
      {
         return list.HasIndex(index) ? list[index] : default(T);
      }
   
      public static bool TryGet<T>(this IList<T> list, int index, out T value)
      {
         var hasIndex = list.HasIndex(index);
         value = hasIndex ? list[index] : default(T);
         return hasIndex;
      }
   
      public static IList<T> Clone<T>(this IList<T> list)
      {
         var newList = new List<T>();
         newList.AddRange(list);
         return newList;
      }

      public static void Shuffle<T>(this List<T> list)
      {
         Random rng = new Random();
         var listB = list.OrderBy(x => rng.Next()).ToList();
         list.Clear();
         list.AddRange(listB);
      }

      public static T FindRandom<T>(this List<T> list, Predicate<T> match)
      {
         var newList = new List<T>();
         newList.AddRange(list);
         newList.Shuffle();
         return newList.Find(match);
      }
   }
}
