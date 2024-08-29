using UnityEngine;

namespace DataKeeper.SingletonPattern
{
    public class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        private static T _instance;
        public static T Instance => _instance ??= CreateInstance();

        private static Transform _container;
        private static Transform Container => _container ??= CreateContainer();
     
        
        private static T CreateInstance()
        {
            var go = new GameObject($"{typeof(T).Name} (Singleton)");
            go.transform.parent = Container;
            return go.AddComponent<T>();
        }

        private static Transform CreateContainer()
        {
            var go = new GameObject($"[Singletons]");
            DontDestroyOnLoad(go);
            return go.transform;
        }
    }
}

