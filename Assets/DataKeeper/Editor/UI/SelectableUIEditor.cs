using DataKeeper.Components.UI;
using UnityEditor;
using UnityEditor.UI;
using UnityEngine.UI;

namespace DataKeeper.Editor.UI
{
    [CustomEditor(typeof(SelectableUI), true)]
    /// <summary>
    ///   Custom Editor for the Selectable Component.
    ///   Extend this class to write a custom editor for an Selectable-derived component.
    /// </summary>
    public class SelectableUIEditor : SelectableEditor
    {
        SerializedProperty _overrideTransitionColorProperty;
        SerializedProperty _transitionProperty;
        
        static Selectable.Transition GetTransition(SerializedProperty transition)
        {
            return (Selectable.Transition)transition.enumValueIndex;
        }
        
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
                EditorGUILayout.PropertyField(_overrideTransitionColorProperty);
            }

            serializedObject.ApplyModifiedProperties();
        }
        
        //  public override void OnInspectorGUI()
        // {
        //     serializedObject.Update();
        //
        //     EditorGUILayout.PropertyField(m_InteractableProperty);
        //
        //     var trans = GetTransition(m_TransitionProperty);
        //
        //     var graphic = m_TargetGraphicProperty.objectReferenceValue as Graphic;
        //     if (graphic == null)
        //         graphic = (target as Selectable).GetComponent<Graphic>();
        //
        //     var animator = (target as Selectable).GetComponent<Animator>();
        //     m_ShowColorTint.target = (!m_TransitionProperty.hasMultipleDifferentValues && trans == Button.Transition.ColorTint);
        //     m_ShowSpriteTrasition.target = (!m_TransitionProperty.hasMultipleDifferentValues && trans == Button.Transition.SpriteSwap);
        //     m_ShowAnimTransition.target = (!m_TransitionProperty.hasMultipleDifferentValues && trans == Button.Transition.Animation);
        //
        //     EditorGUILayout.PropertyField(m_TransitionProperty);
        //
        //     ++EditorGUI.indentLevel;
        //     {
        //         if (trans == Selectable.Transition.ColorTint || trans == Selectable.Transition.SpriteSwap)
        //         {
        //             EditorGUILayout.PropertyField(m_TargetGraphicProperty);
        //         }
        //
        //         switch (trans)
        //         {
        //             case Selectable.Transition.ColorTint:
        //                 if (graphic == null)
        //                     EditorGUILayout.HelpBox("You must have a Graphic target in order to use a color transition.", MessageType.Warning);
        //                 break;
        //
        //             case Selectable.Transition.SpriteSwap:
        //                 if (graphic as Image == null)
        //                     EditorGUILayout.HelpBox("You must have a Image target in order to use a sprite swap transition.", MessageType.Warning);
        //                 break;
        //         }
        //
        //         if (EditorGUILayout.BeginFadeGroup(m_ShowColorTint.faded))
        //         {
        //             EditorGUILayout.PropertyField(m_ColorBlockProperty);
        //         }
        //         EditorGUILayout.EndFadeGroup();
        //
        //         if (EditorGUILayout.BeginFadeGroup(m_ShowSpriteTrasition.faded))
        //         {
        //             EditorGUILayout.PropertyField(m_SpriteStateProperty);
        //         }
        //         EditorGUILayout.EndFadeGroup();
        //
        //         if (EditorGUILayout.BeginFadeGroup(m_ShowAnimTransition.faded))
        //         {
        //             EditorGUILayout.PropertyField(m_AnimTriggerProperty);
        //
        //             if (animator == null || animator.runtimeAnimatorController == null)
        //             {
        //                 Rect buttonRect = EditorGUILayout.GetControlRect();
        //                 buttonRect.xMin += EditorGUIUtility.labelWidth;
        //                 if (GUI.Button(buttonRect, "Auto Generate Animation", EditorStyles.miniButton))
        //                 {
        //                     var controller = GenerateSelectableAnimatorContoller((target as Selectable).animationTriggers, target as Selectable);
        //                     if (controller != null)
        //                     {
        //                         if (animator == null)
        //                             animator = (target as Selectable).gameObject.AddComponent<Animator>();
        //
        //                         Animations.AnimatorController.SetAnimatorController(animator, controller);
        //                     }
        //                 }
        //             }
        //         }
        //         EditorGUILayout.EndFadeGroup();
        //     }
        //     --EditorGUI.indentLevel;
        //
        //     EditorGUILayout.Space();
        //
        //     EditorGUILayout.PropertyField(m_NavigationProperty);
        //
        //     EditorGUI.BeginChangeCheck();
        //     Rect toggleRect = EditorGUILayout.GetControlRect();
        //     toggleRect.xMin += EditorGUIUtility.labelWidth;
        //     s_ShowNavigation = GUI.Toggle(toggleRect, s_ShowNavigation, m_VisualizeNavigation, EditorStyles.miniButton);
        //     if (EditorGUI.EndChangeCheck())
        //     {
        //         EditorPrefs.SetBool(s_ShowNavigationKey, s_ShowNavigation);
        //         SceneView.RepaintAll();
        //     }
        //
        //     // We do this here to avoid requiring the user to also write a Editor for their Selectable-derived classes.
        //     // This way if we are on a derived class we dont draw anything else, otherwise draw the remaining properties.
        //     ChildClassPropertiesGUI();
        //
        //     serializedObject.ApplyModifiedProperties();
        // }
    }
}