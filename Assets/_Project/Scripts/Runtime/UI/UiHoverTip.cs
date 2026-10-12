using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Specimen.Runtime.UI
{
    /// <summary>Muestra un tooltip (texto calculado al pasar el ratón) y avisa de clics izquierdo/derecho.</summary>
    public sealed class UiHoverTip : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
    {
        public Func<string> Text;
        public Action LeftClick;
        public Action RightClick;

        public void OnPointerEnter(PointerEventData eventData)
        {
            string text = Text?.Invoke();
            if (!string.IsNullOrEmpty(text)) UiTooltip.Show(text);
        }

        public void OnPointerExit(PointerEventData eventData) => UiTooltip.Hide();

        public void OnPointerClick(PointerEventData eventData)
        {
            if (eventData.button == PointerEventData.InputButton.Right) RightClick?.Invoke();
            else LeftClick?.Invoke();
            string text = Text?.Invoke();
            if (!string.IsNullOrEmpty(text)) UiTooltip.Show(text);
        }
    }
}
