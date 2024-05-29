using UnityEngine;
using UnityEngine.EventSystems;

namespace DataKeeper.Components.UI
{
    public class ResizeHandle : MonoBehaviour, IDragHandler, IPointerDownHandler
    {
        [SerializeField] private RectTransform _targetRect;
        [SerializeField] private Vector2 _minSize;
        [SerializeField] private bool _asLastSibling = true;
        private Vector2 _invertY = new Vector2(1, -1);
        private Vector2 _targetPos;

        private void Awake()
        {
            _targetRect.pivot = Vector2.up;
        }

        public void OnPointerDown(PointerEventData eventData)
        {
            if(_asLastSibling)
                _targetRect.SetAsLastSibling();
            
            _targetPos = _targetRect.position;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _targetRect.sizeDelta = Vector2.Max(_minSize, (eventData.position - _targetPos) * _invertY);
        }
    }
}
