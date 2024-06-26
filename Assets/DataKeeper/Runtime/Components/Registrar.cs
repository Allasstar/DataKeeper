using System;
using System.Collections.Generic;
using DataKeeper.Generic;
using UnityEngine;

namespace DataKeeper.Components
{
    [DefaultExecutionOrder(-10000)]
    [AddComponentMenu("DataKeeper/Registrar", 0)]
    public class Registrar : MonoBehaviour
    {
        public static Register<Component> Components = new ();

        private static int _scene = -1; 

        [SerializeField] private List<ComponentId> _register = new();
        
        private void Awake()
        {
            if (!_scene.Equals(gameObject.scene.buildIndex))
            {
                Components.ClearNull();
                _scene = gameObject.scene.buildIndex;
            }
            
            foreach (var c in _register)
            {
                Components.Reg(c.component, c.id);
            }
        }
        
        [Serializable]
        public class ComponentId
        {
            public string id;
            public Component component;
        }
    }
}
