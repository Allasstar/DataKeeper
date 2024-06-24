using UnityEditor;
using UnityEditor.UI;
using UnityEngine;
using UnityEngine.UI;

public class SelectableUIEditor : SelectableEditor
{
    SerializedProperty _overrideTransitionColorProperty;
    SerializedProperty _transitionProperty;
    
    protected override void OnEnable()
    {
        base.OnEnable();
        
        _overrideTransitionColorProperty = serializedObject.FindProperty("_overrideTransitionColor");
        _transitionProperty = serializedObject.FindProperty("m_Transition");
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        
        if (GetTransition(_transitionProperty) == Selectable.Transition.ColorTint)
        {
            EditorGUILayout.Space();
            
            // Draw the foldout for the Optional<SelectableColorPalette>
            EditorGUILayout.PropertyField(_overrideTransitionColorProperty);
            
            // Get the SelectableColorPalette value
            SerializedProperty colorPaletteProperty = _overrideTransitionColorProperty.FindPropertyRelative("value");
            
            if (_overrideTransitionColorProperty.FindPropertyRelative("enabled").boolValue 
                && colorPaletteProperty.objectReferenceValue  != null)
            {
                EditorGUI.indentLevel++;
                
                // Draw here all fields from assigned to value scriptable object
                DrawPropertiesOfScriptableObject(colorPaletteProperty.objectReferenceValue);
                
                EditorGUI.indentLevel--;
            }
        }
    
        serializedObject.ApplyModifiedProperties();
    }
    
    static Selectable.Transition GetTransition(SerializedProperty transition)
    {
        return (Selectable.Transition)transition.enumValueIndex;
    }
    
    void DrawPropertiesOfScriptableObject(Object scriptableObject)
    {
        SerializedObject serializedObject = new SerializedObject(scriptableObject);
        SerializedProperty property = serializedObject.GetIterator();
        property.NextVisible(true); // Skip the script field

        while (property.NextVisible(false))
        {
            EditorGUILayout.PropertyField(property, true);
        }

        serializedObject.ApplyModifiedProperties();
    }
}