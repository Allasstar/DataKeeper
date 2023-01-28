using UnityEngine;

namespace ActionStage.Core.Init
{
    public static class Initializator
    {
        private static InitializeSO InitializeSO = new InitializeSO();
        
        [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
        private static void OnBeforeSceneLoadRuntimeMethod()
        {
            
        }
    }
}
