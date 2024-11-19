using Unity.Mouse;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DesktopUI.Unity
{
    [RequireComponent(typeof(RectTransform))]
    public class DraggableBehaviour : MonoBehaviour
    {
        [SerializeField] private DragDetectionProvider dragDetectionProvider = null;

        [SerializeField] private ScreenPointerHandler screenPointerHandler = default;

        private Vector2 _centerPoint;
        private DragManager _dragManager;
        private Vector2 WorldCenterPoint => transform.TransformPoint(_centerPoint);

        private bool _isDragging = false;

        //TODO Add Dependency injection to obtain IScreenPointerHandler

        private void Awake()
        {
            dragDetectionProvider ??= gameObject.AddComponent<DragDetectionProvider>();

            _dragManager = GetComponentInParent<DragManager>();

            if (_dragManager == null)
            {
                Debug.LogError($"Could not find {nameof(DragManager)} component in parent");
            }

            dragDetectionProvider.OnDragStarted += OnDragStarted;
            dragDetectionProvider.OnDragContinue += OnDragContinue;
            dragDetectionProvider.OnDragEnded += OnDragEnded;

            if (screenPointerHandler != null) //TODO Refactor when DI
            {
                screenPointerHandler.OnPointerExited += OnDragEnded;
            }
        }

        private void OnDragStarted(PointerEventData eventData)
        {
            _dragManager.RegisterDraggedObject(this);
            
            if (screenPointerHandler != null) //TODO Refactor when DI
            {
                screenPointerHandler.OnPointerExited += OnDragEnded;
            }
            
            _isDragging = true;
        }

        private void OnDragContinue(PointerEventData eventData)
        {
            if (_isDragging && _dragManager.IsWithinBounds(WorldCenterPoint + eventData.delta))
            {
                transform.Translate(eventData.delta);
            }
        }

        private void OnDragEnded(PointerEventData eventData)
        {
            _isDragging = false;
            
            if (screenPointerHandler != null) //TODO Refactor when DI
            {
                screenPointerHandler.OnPointerExited -= OnDragEnded;
            }
            
            _dragManager.UnregisterDraggedObject(this);
        }

        private void OnDestroy()
        {
            dragDetectionProvider.OnDragStarted -= OnDragStarted;
            dragDetectionProvider.OnDragContinue -= OnDragContinue;
            dragDetectionProvider.OnDragEnded -= OnDragEnded;

            screenPointerHandler.OnPointerExited -= OnDragEnded;
        }
    }
}