using System.Collections.Generic;
using DataKeeper.Core.Primitives;
using UnityEngine;

namespace DataKeeper.Core.Components
{
    [DefaultExecutionOrder(-10000)]
    public class RegisterComponent : MonoBehaviour
    {
        public static Register<Component> Components = new();

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
    }
}
