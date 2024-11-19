using DesktopUI.Unity.Events;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Unity.Mouse
{
    public class ScreenPointerHandler : MonoBehaviour,
        IPointerEnterHandler,
        IPointerExitHandler,
        IScreenPointerHandler
    {
        public event PointerEvent OnPointerEntered;
        public event PointerEvent OnPointerExited;

        public void OnPointerEnter(PointerEventData eventData)
        {
            OnPointerEntered?.Invoke(eventData);
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            OnPointerExited?.Invoke(eventData);
        }
    }
}