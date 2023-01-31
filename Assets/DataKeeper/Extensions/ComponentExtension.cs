using UnityEngine;

namespace DataKeeper.Extensions
{
    public static class ComponentExtension
    {
        public static void SetParentGameObjectActive(this Component component, bool isActive)
        {
            if (component == null) return;
            component.transform.parent.gameObject.SetActive(isActive);
        }
        
        public static void SetGameObjectActive(this Component component, bool isActive)
        {
            if (component == null) return;
            component.gameObject.SetActive(isActive);
        }
    }
}
