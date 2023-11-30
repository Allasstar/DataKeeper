using System.Collections.Generic;
using DataKeeper.Base;
using UnityEditor;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace DataKeeper.Extra
{
    [CreateAssetMenu(menuName = "DataKeeper/Bootstrap SO", fileName = "Bootstrap SO")]
    public class BootstrapSO : SO
    {
        [SerializeField] private List<SceneAsset> _bootstrapSceneList = new();
        
        [SerializeField, Space(20)] private bool _loadInitialSceneInEditor = false;
        [SerializeField] private bool _loadInitialSceneAdditive = false;
        [SerializeField] private SceneAsset _initialScene;
        
        [SerializeField, Space(20)] private List<GameObject> _dontDestroyOnLoadList = new();

        private int _bootstrapSceneCount = 0;
        
        public override void Initialize()
        {
            Boot();
            Init();
            InstantiatePrefabs();
        }

        private void Boot()
        {
            if (_bootstrapSceneList.Count > 0)
            {
                _bootstrapSceneCount = 0;
                SceneManager.sceneLoaded += SceneManagerOnSceneLoaded;

                foreach (var bootstrapScene in _bootstrapSceneList)
                {
                    if(bootstrapScene == null) continue;
                    
                    _bootstrapSceneCount++;
                    SceneManager.LoadScene(bootstrapScene.name, LoadSceneMode.Additive);
                }
            }
        }
        
        private void Init()
        {
            if (!_loadInitialSceneInEditor || _initialScene == null) return;
            
            SceneManager.LoadScene(_initialScene.name, _loadInitialSceneAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single);
        }

        private void InstantiatePrefabs()
        {
            foreach (var gameObject in _dontDestroyOnLoadList)
            {
                var go = Instantiate(gameObject);
                go.name = gameObject.name;
                DontDestroyOnLoad(go);
            }
        }
        
        private void SceneManagerOnSceneLoaded(Scene scene, LoadSceneMode mode)
        {
            if (_bootstrapSceneList.Exists(e => e.name == scene.name))
            {
                _bootstrapSceneCount--;
                SceneManager.UnloadSceneAsync(scene.name);
            }

            if (_bootstrapSceneCount <= 0)
            {
                SceneManager.sceneLoaded -= SceneManagerOnSceneLoaded;
            }
        }
    }
}
