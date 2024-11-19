using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace DesktopUI.Unity
{
    [RequireComponent(typeof(RectTransform))]
    public class FocusableBehaviour : MonoBehaviour, IPointerDownHandler
    {
        public void OnPointerDown(PointerEventData eventData)
        {
            transform.SetAsLastSibling();
        }
    }
}