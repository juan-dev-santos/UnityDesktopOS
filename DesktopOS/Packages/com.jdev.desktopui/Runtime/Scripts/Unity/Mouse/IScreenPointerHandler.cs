using DesktopUI.Unity.Events;

namespace Unity.Mouse
{
    public interface IScreenPointerHandler
    {
        event PointerEvent OnPointerEntered;
        event PointerEvent OnPointerExited;
    }
}