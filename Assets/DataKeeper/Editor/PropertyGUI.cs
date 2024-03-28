using DataKeeper.Generic;
using UnityEditor;
using UnityEngine;

namespace DataKeeper.Editor
{
    public static class PropertyGUI
    {
        public static void DrawGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.type == typeof(Optional<>).Name)
            {
                DrawGUIOptional(position, property, label);
                return;
            }
            
            EditorGUI.PropertyField(position, property, label, true);
        }
        
        public static float GetPropertyHeight(SerializedProperty property)
        {
            if (property.type == typeof(Optional<>).Name)
            {
                return PropertyHeightOptional(property);
            }
            
            return EditorGUI.GetPropertyHeight(property);
        }
        
        // --- GUI ---
        
        private static void DrawGUIOptional(Rect position, SerializedProperty property, GUIContent label)
        {
            var valueProperty = property.FindPropertyRelative("value");
            var enabledProperty = property.FindPropertyRelative("enabled");

            EditorGUI.BeginProperty(position, label, property);
            position.width -= 24;
            EditorGUI.BeginDisabledGroup(!enabledProperty.boolValue);
            EditorGUI.PropertyField(position, valueProperty, label, true);
            EditorGUI.EndDisabledGroup();

            int indent = EditorGUI.indentLevel;
            EditorGUI.indentLevel = 0;
            position.x += position.width + 24;
            position.width = position.height = EditorGUI.GetPropertyHeight(enabledProperty);
            position.x -= position.width;
            EditorGUI.PropertyField(position, enabledProperty, GUIContent.none);
            EditorGUI.indentLevel = indent;
            EditorGUI.EndProperty();
        }

        private static float PropertyHeightOptional(SerializedProperty property)
        {
            var valueProperty = property.FindPropertyRelative("value");
            return EditorGUI.GetPropertyHeight(valueProperty);
        }
    }
}