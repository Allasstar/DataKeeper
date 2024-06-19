using System;
using DataKeeper.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace DataKeeper.UI
{
    [AddComponentMenu("UI/DataKeeper/Toggle")]
    [RequireComponent(typeof(RectTransform))]
    public class ToggleUI : Selectable, IPointerClickHandler, ISubmitHandler, ICanvasElement
    {
        [field: SerializeField, Space]
        public Reactive<bool> onValueChanged { private set; get; } = new Reactive<bool>();

        [SerializeField] private ToggleUIGroup _group;

   
        [field: SerializeField, Space] public Image Image { private set; get; }
        public Optional<ToggleSprite> _imageSprite = new Optional<ToggleSprite>();
        public Optional<ToggleColor> _imageColor = new Optional<ToggleColor>();
    
        [field: SerializeField, Space] public TextMeshProUGUI Text { private set; get; }
        public Optional<ToggleColor> _textColor = new Optional<ToggleColor>();

        public bool isOn
        {
            get => onValueChanged.Value;
            set => Set(value);
        }

        private void Set(bool value)
        {
            if (onValueChanged.Value == value)
                return;
            
            onValueChanged.SilentValue = value;

            UpdateGroup();
            UpdateUI();

            if (Application.isPlaying)
                onValueChanged.Invoke(); 
        }

        public ToggleUIGroup group
        {
            get { return _group; }
            set
            {
                SetToggleGroup(value, true);
                UpdateUI();
            }
        }

        private void Start()
        {
            UpdateUI();
        }
        
        protected override void OnDestroy()
        {
            if (_group != null)
                _group.EnsureValidState();
            base.OnDestroy();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            SetToggleGroup(_group, false);
            UpdateUI();
        }

        protected override void OnDisable()
        {
            SetToggleGroup(null, false);
            base.OnDisable();
        }
        
        private void SetToggleGroup(ToggleUIGroup newGroup, bool setMemberValue)
        {
            // Sometimes IsActive returns false in OnDisable so don't check for it.
            // Rather remove the toggle too often than too little.
            if (_group != null)
                _group.UnregisterToggle(this);

            // At runtime the group variable should be set but not when calling this method from OnEnable or OnDisable.
            // That's why we use the setMemberValue parameter.
            if (setMemberValue)
                _group = newGroup;

            // Only register to the new group if this Toggle is active.
            if (newGroup != null && IsActive())
                newGroup.RegisterToggle(this);

            // If we are in a new group, and this toggle is on, notify group.
            // Note: Don't refer to m_Group here as it's not guaranteed to have been set.
            if (newGroup != null && isOn && IsActive())
                newGroup.NotifyToggleOn(this);
        }

        private void InternalToggle()
        {
            if (!IsActive() || !IsInteractable())
                return;

            isOn = !isOn;
            UpdateUI();
        }
        
        private void UpdateGroup()
        {
            if (_group != null && _group.isActiveAndEnabled && IsActive())
            {
                if (onValueChanged.Value || (!_group.AnyTogglesOn() && !_group.allowSwitchOff))
                {
                    onValueChanged.SilentValue = true;
                    _group.NotifyToggleOn(this);
                }
            }
        }
        
        private void UpdateUI()
        {
            if (Text != null && _textColor.Enabled)
            {
                Text.color = isOn ? _textColor.Value.On : _textColor.Value.Off;
            }
            
            if(this.Image == null) return;
            
            if (_imageSprite.Enabled)
            {
                this.Image.sprite = isOn ? _imageSprite.Value.On : _imageSprite.Value.Off;
            }
            
            if (_imageColor.Enabled)
            {
                this.Image.color = isOn ? _imageColor.Value.On : _imageColor.Value.Off;
            }
        }
        
        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button != PointerEventData.InputButton.Left)
                return;

            InternalToggle();
        }

        public void OnSubmit(BaseEventData eventData)
        {
            InternalToggle();
        }

        public void Rebuild(CanvasUpdate executing)
        {
        }

        public void LayoutComplete()
        {
        }

        public void GraphicUpdateComplete()
        {
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
        
#if UNITY_EDITOR
        protected override void OnValidate()
        {
            base.OnValidate();

            if (!UnityEditor.PrefabUtility.IsPartOfPrefabAsset(this) && !Application.isPlaying)
                CanvasUpdateRegistry.RegisterCanvasElementForLayoutRebuild(this);

            SetToggleGroup(_group, false);
            UpdateGroup();
            UpdateUI();
        }

#endif // if UNITY_EDITOR
    }
}


        
