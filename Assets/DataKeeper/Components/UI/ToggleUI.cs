using System;
using DataKeeper.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DataKeeper.Components.UI
{
    [AddComponentMenu("DataKeeper/UI/Toggle UI")]
    [RequireComponent(typeof(RectTransform))]
    public class ToggleUI : Selectable, IPointerClickHandler, ISubmitHandler, ICanvasElement
    {
        
        // Whether the toggle is on
        [Tooltip("Is the toggle currently on or off?")]
        [SerializeField, Space]
        private bool m_IsOn;
        
        [SerializeField]
        private ToggleUIGroup m_Group;
        
        [field: SerializeField, Space] public Image Icon { private set; get; }
        public Optional<ToggleSprite> _iconSprite = new Optional<ToggleSprite>();
        public Optional<ToggleColor> _iconColor = new Optional<ToggleColor>();
    
        [field: SerializeField, Space] public TextMeshProUGUI Text { private set; get; }
        public Optional<ToggleColor> _textColor = new Optional<ToggleColor>();
        
        [Space]
        public UnityEvent<bool> onValueChanged = new UnityEvent<bool>();
        
        private void UpdateUI()
        {
            if (Text != null && _textColor.Enabled)
            {
                Text.color = m_IsOn ? _textColor.Value.On : _textColor.Value.Off;
            }
            
            if(Icon == null) return;
            
            if (_iconSprite.Enabled)
            {
                Icon.sprite = m_IsOn ? _iconSprite.Value.On : _iconSprite.Value.Off;
            }
            
            if (_iconColor.Enabled)
            {
                Icon.color = m_IsOn ? _iconColor.Value.On : _iconColor.Value.Off;
            }
        }

        /// <summary>
        /// Group the toggle belongs to.
        /// </summary>
        public ToggleUIGroup group
        {
            get { return m_Group; }
            set
            {
                SetToggleGroup(value, true);
                UpdateUI();
            }
        }

        protected ToggleUI()
        {}

#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();
            
            SetToggleGroup(m_Group, false);
            if (group != null && IsActive())
            {
                if (isOn || (!group.AnyTogglesOn() && !group.allowSwitchOff))
                {
                    isOn = true;
                    group.NotifyToggleOn(this);
                }
            }
            UpdateUI();
            if (!UnityEditor.PrefabUtility.IsPartOfPrefabAsset(this) && !Application.isPlaying)
                CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);
        }

#endif // if UNITY_EDITOR

        public virtual void Rebuild(CanvasUpdate executing)
        {
#if UNITY_EDITOR
            if (executing == CanvasUpdate.Prelayout)
                onValueChanged.Invoke(m_IsOn);
#endif
        }

        public virtual void LayoutComplete()
        {}

        public virtual void GraphicUpdateComplete()
        {}

        protected override void OnDestroy()
        {
            if (m_Group != null)
                m_Group.EnsureValidState();
            base.OnDestroy();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            SetToggleGroup(m_Group, false);
            UpdateUI();
        }

        protected override void OnDisable()
        {
            SetToggleGroup(null, false);
            base.OnDisable();
        }

        protected override void OnDidApplyAnimationProperties()
        {
            // Check if isOn has been changed by the animation.
            // Unfortunately there is no way to check if we don�t have a graphic.
            // if (graphic != null)
            // {
            //     bool oldValue = !Mathf.Approximately(graphic.canvasRenderer.GetColor().a, 0);
            //     if (m_IsOn != oldValue)
            //     {
            //         m_IsOn = oldValue;
            //         Set(!oldValue);
            //     }
            // }
            //
            // base.OnDidApplyAnimationProperties();
        }

        private void SetToggleGroup(ToggleUIGroup newGroup, bool setMemberValue)
        {
            // Sometimes IsActive returns false in OnDisable so don't check for it.
            // Rather remove the toggle too often than too little.
            if (m_Group != null)
                m_Group.UnregisterToggle(this);

            // At runtime the group variable should be set but not when calling this method from OnEnable or OnDisable.
            // That's why we use the setMemberValue parameter.
            if (setMemberValue)
                m_Group = newGroup;

            // Only register to the new group if this Toggle is active.
            if (newGroup != null && IsActive())
                newGroup.RegisterToggle(this);

            // If we are in a new group, and this toggle is on, notify group.
            // Note: Don't refer to m_Group here as it's not guaranteed to have been set.
            if (newGroup != null && isOn && IsActive())
                newGroup.NotifyToggleOn(this);
        }

        /// <summary>
        /// Whether the toggle is currently active.
        /// </summary>
        /// <example>
        /// <code>
        /// <![CDATA[
        /// /Attach this script to a Toggle GameObject. To do this, go to Create>UI>Toggle.
        /// //Set your own Text in the Inspector window
        ///
        /// using UnityEngine;
        /// using UnityEngine.UI;
        ///
        /// public class Example : MonoBehaviour
        /// {
        ///     Toggle m_Toggle;
        ///     public Text m_Text;
        ///
        ///     void Start()
        ///     {
        ///         //Fetch the Toggle GameObject
        ///         m_Toggle = GetComponent<Toggle>();
        ///         //Add listener for when the state of the Toggle changes, and output the state
        ///         m_Toggle.onValueChanged.AddListener(delegate {
        ///                 ToggleValueChanged(m_Toggle);
        ///             });
        ///
        ///         //Initialize the Text to say whether the Toggle is in a positive or negative state
        ///         m_Text.text = "Toggle is : " + m_Toggle.isOn;
        ///     }
        ///
        ///     //Output the new state of the Toggle into Text when the user uses the Toggle
        ///     void ToggleValueChanged(Toggle change)
        ///     {
        ///         m_Text.text =  "Toggle is : " + m_Toggle.isOn;
        ///     }
        /// }
        /// ]]>
        ///</code>
        /// </example>

        public bool isOn
        {
            get { return m_IsOn; }

            set
            {
                Set(value);
            }
        }

        /// <summary>
        /// Set isOn without invoking onValueChanged callback.
        /// </summary>
        /// <param name="value">New Value for isOn.</param>
        public void SetIsOnWithoutNotify(bool value)
        {
            Set(value, false);
        }

        public void Set(bool value, bool sendCallback = true)
        {
            if (m_IsOn == value)
                return;

            // if we are in a group and set to true, do group logic
            m_IsOn = value;
            if (m_Group != null && m_Group.isActiveAndEnabled && IsActive())
            {
                if (m_IsOn || (!m_Group.AnyTogglesOn() && !m_Group.allowSwitchOff))
                {
                    m_IsOn = true;
                    m_Group.NotifyToggleOn(this, sendCallback);
                }
            }

            // Always send event when toggle is clicked, even if value didn't change
            // due to already active toggle in a toggle group being clicked.
            // Controls like Dropdown rely on this.
            // It's up to the user to ignore a selection being set to the same value it already was, if desired.
            UpdateUI();
            if (sendCallback)
            {
                UISystemProfilerApi.AddMarker("ToggleUI.value", this);
                onValueChanged.Invoke(m_IsOn);
            }
        }

        /// <summary>
        /// Assume the correct visual state.
        /// </summary>
        protected override void Start()
        {
            UpdateUI();
        }

        private void InternalToggle()
        {
            if (!IsActive() || !IsInteractable())
                return;

            isOn = !isOn;
        }

        /// <summary>
        /// React to clicks.
        /// </summary>
        public virtual void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            InternalToggle();
        }

        public virtual void OnSubmit(BaseEventData eventData)
        {
            InternalToggle();
        }
        
        [Serializable]
        public class ToggleSprite
        {
            [field: SerializeField] public Sprite On { private set; get; }
            [field: SerializeField] public Sprite Off { private set; get; }
        }
       
        [Serializable]
        public class ToggleColor
        {
            [field: SerializeField] public Color On { private set; get; } = Color.white;
            [field: SerializeField] public Color Off { private set; get; } = Color.white;
        }

    }
}


        
