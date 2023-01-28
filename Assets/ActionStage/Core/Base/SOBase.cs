using UnityEngine;

namespace ActionStage.Core.Base
{
    public abstract class SOBase : ScriptableObject
    {
        public abstract void Initialize();

#if UNITY_EDITOR
        [ContextMenu("Save")]
        private void Save()
        {
            UnityEditor.EditorUtility.SetDirty(this);
            UnityEditor.AssetDatabase.SaveAssets();
        }
#endif
    }
}
