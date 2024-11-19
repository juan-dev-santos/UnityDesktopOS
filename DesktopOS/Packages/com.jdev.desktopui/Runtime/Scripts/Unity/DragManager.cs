using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

namespace DesktopUI.Unity
{
    public class DragManager : MonoBehaviour
    {
        [SerializeField] private RectTransform defaultLayer = null;
        [SerializeField] private RectTransform dragLayer = null;

        private Rect _boundingBox;

        private DraggableBehaviour _currentDraggedObject = null;
        public DraggableBehaviour CurrentDraggedObject => _currentDraggedObject;

        private void Awake()
        {
            SetBoundingBoxRect(dragLayer);
        }

        public void RegisterDraggedObject(DraggableBehaviour drag)
        {
            _currentDraggedObject = drag;
            drag.transform.SetParent(dragLayer);
        }

        public void UnregisterDraggedObject(DraggableBehaviour drag)
        {
            drag.transform.SetParent(defaultLayer);
            _currentDraggedObject = null;
        }

        public bool IsWithinBounds(Vector2 position)
        {
            return _boundingBox.Contains(position);
        }

        private void SetBoundingBoxRect(RectTransform rectTransform)
        {
            var corners = new Vector3[4];
            rectTransform.GetWorldCorners(corners);
            var position = corners[0];

            Vector2 size = new Vector2(
                rectTransform.lossyScale.x * rectTransform.rect.size.x,
                rectTransform.lossyScale.y * rectTransform.rect.size.y);

            _boundingBox = new Rect(position, size);
        }
    }
}