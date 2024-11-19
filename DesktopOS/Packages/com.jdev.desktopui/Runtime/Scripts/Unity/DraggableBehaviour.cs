using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DesktopUI.Unity
{
    [RequireComponent(typeof(RectTransform))]
    public class DraggableBehaviour : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private DragManager _dragManager = default;
        
        private Vector2 _centerPoint;
        private Vector2 WorldCenterPoint => transform.TransformPoint(_centerPoint);

        private void Awake()
        {
            _dragManager = GetComponentInParent<DragManager>();

            if (_dragManager == null)
            {
                Debug.LogError($"Could not find {nameof(DragManager)} component in parent");
            }
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _dragManager.RegisterDraggedObject(this);
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_dragManager.IsWithinBounds(WorldCenterPoint + eventData.delta))
            {
                transform.Translate(eventData.delta);
            }
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            _dragManager.UnregisterDraggedObject(this);
        }
    }
}