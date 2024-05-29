using System;
using System.Collections;
using UnityEngine;

namespace DataKeeper.ActCore
{
    public static class ActEnumerator
    {
        public static IEnumerator WaitUntil(Func<bool> wait, Action callback)
        {
            yield return new WaitUntil(wait);
            callback?.Invoke();
        }
    
        public static IEnumerator WaitWhile(Func<bool> wait, Action callback)
        {
            yield return new WaitWhile(wait);
            callback?.Invoke();
        }
    
        public static IEnumerator OneSecondUpdate(Action callback)
        {
            
            while (true)
            {
                callback?.Invoke();
                yield return new WaitForSeconds(1);
            }
        }
    
        public static IEnumerator WaitSeconds(float time, Action callback)
        {
            if (time == 0)
            {
                yield return new WaitForEndOfFrame();
            }
            else if (time > 0)
            {
                yield return new WaitForSeconds(time);
            }
            callback?.Invoke();
        }
    
        public static IEnumerator Int(int from, int to, float duration, Action<int> value, Action onComplete)
        {
            var time = 0f;
            value?.Invoke(from);
            
            while (time <= duration)
            {
                yield return new WaitForEndOfFrame();
                time += Time.deltaTime;
                
                value?.Invoke(Lerp.Int(from, to, time / duration));
            }
            
            value?.Invoke(to);
            onComplete?.Invoke();
        }
    
        public static IEnumerator Delta(float duration, Action<float> delta, Action onComplete)
        {
            var time = 0f;
            
            while (time <= duration)
            {
                yield return new WaitForEndOfFrame();
                time += Time.deltaTime;
                delta?.Invoke(Time.deltaTime);
            }
            
            onComplete?.Invoke();
        }
        
        public static IEnumerator Float(float from, float to, float duration, Action<float> value, Action onComplete)
        {
            var time = 0f;
            value?.Invoke(from);
            
            while (time <= duration)
            {
                yield return new WaitForEndOfFrame();
                time += Time.deltaTime;
                
                value?.Invoke(Lerp.Float(from, to, time / duration));
            }
            
            value?.Invoke(to);
            onComplete?.Invoke();
        }
        
        public static IEnumerator Float(float from, float to, float duration, Func<float, float, float, FloatEase> ease, Action<float> value, Action onComplete)
        {
            var time = 0f;
            value?.Invoke(from);
            
            while (time <= duration)
            {
                yield return new WaitForEndOfFrame();
                time += Time.deltaTime;
                
                value?.Invoke(ease(time / duration, from, to));
            }
            
            value?.Invoke(to);
            onComplete?.Invoke();
        }
    }
}
