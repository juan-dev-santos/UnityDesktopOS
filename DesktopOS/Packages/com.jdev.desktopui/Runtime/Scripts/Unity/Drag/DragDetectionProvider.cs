using DesktopUI.Unity.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DesktopUI.Unity
{
    [RequireComponent(typeof(RectTransform))]
    public class DragDetectionProvider : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        public event PointerEvent OnDragStarted;
        public event PointerEvent OnDragContinue;
        public event PointerEvent OnDragEnded;

        private Vector2 _centerPoint;
        private Vector2 WorldCenterPoint => transform.TransformPoint(_centerPoint);

        public void OnBeginDrag(UnityEngine.EventSystems.PointerEventData eventData)
        {
            OnDragStarted?.Invoke(eventData);
        }

        public void OnDrag(UnityEngine.EventSystems.PointerEventData eventData)
        {
            OnDragContinue?.Invoke(eventData);
        }

        public void OnEndDrag(UnityEngine.EventSystems.PointerEventData eventData)
        {
            OnDragEnded?.Invoke(eventData);
        }
    }
}