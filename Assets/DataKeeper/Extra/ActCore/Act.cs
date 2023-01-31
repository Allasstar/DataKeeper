using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;

namespace DataKeeper.Extra.ActCore
{
    public static class Act
    {
        private static ActEngine _actEngine;

        public static ActEngine Engine
        {
            get
            {
                Init();
                return _actEngine;
            }
        }

        public static UnityEvent OnApplicationQuitEvent => Engine.OnApplicationQuitEvent;
        public static UnityEvent<bool> OnApplicationFocusEvent => Engine.OnApplicationFocusEvent;
        public static UnityEvent<bool> OnApplicationPauseEvent => Engine.OnApplicationPauseEvent;
        
        public static UnityEvent<Scene, LoadSceneMode> OnSceneLoadedEvent => Engine.OnSceneLoadedEvent;
        public static UnityEvent<Scene> OnSceneUnloadedEvent => Engine.OnSceneUnloadedEvent;
        
        public static UnityEvent OnUpdateEvent => Engine.OnUpdateEvent;

        
        public static void Init()
        {
            if (_actEngine != null) return;

            GameObject gameObject = new GameObject("[ActEngine]");
            _actEngine = gameObject.AddComponent<ActEngine>();
            Object.DontDestroyOnLoad(gameObject);
        }

        public static void Float(float from, float to, float duration, Action<float> value, Action onComplete = null)
        {
            Engine?.StartCoroutine(FloatTimer(from, to, duration, value, onComplete));
        }
        
        public static void Int(int from, int to, float duration, Action<int> value, Action onComplete = null)
        {
            Engine?.StartCoroutine(IntTimer(from, to, duration, value, onComplete));
        }

        private static IEnumerator FloatTimer(float from, float to, float duration, Action<float> value, Action onComplete)
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
        
        private static IEnumerator IntTimer(int from, int to, float duration, Action<int> value, Action onComplete)
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

        /// <summary>
        /// If time less than 0 - wait 0 seconds. If time equals 0 - wait 1 frame. If time greater than 0 - wait in seconds.
        /// </summary>
        /// <param name="time">in seconds.</param>
        /// <param name="callback">callback on timeout.</param>
        public static void DelayedCall(float time, Action callback)
        {
            StartCoroutine(WaitSeconds(time, callback));
        }
    
        private static IEnumerator WaitSeconds(float time, Action callback)
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
        
        public static Coroutine StartCoroutine(IEnumerator coroutine)
        {
            return Engine?.StartCoroutine(coroutine);
        }
        
        public static void StopCoroutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                Engine?.StopCoroutine(coroutine);
            }
        }
        
        public static Coroutine OneSecondUpdate(Action callback)
        {
            return StartCoroutine(OneSecond(callback));
        }
    
        private static IEnumerator OneSecond(Action callback)
        {
            callback?.Invoke();
            
            while (true)
            {
                yield return new WaitForSeconds(1);
                callback?.Invoke();
            }
        }
        
        public static Coroutine WaitWhile(Func<bool> wait, Action callback)
        {
            return StartCoroutine(WaitWhileProcess(wait, callback));
        }
    
        private static IEnumerator WaitWhileProcess(Func<bool> wait, Action callback)
        {
            yield return new WaitWhile(wait);
            callback?.Invoke();
        }

        public static Coroutine WaitUntil(Func<bool> wait, Action callback)
        {
            return StartCoroutine(WaitUntilProcess(wait, callback));
        }
    
        private static IEnumerator WaitUntilProcess(Func<bool> wait, Action callback)
        {
            yield return new WaitUntil(wait);
            callback?.Invoke();
        }
    }
}