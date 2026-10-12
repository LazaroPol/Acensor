using UnityEngine;
using UnityEngine.EventSystems;

namespace Specimen.Runtime.UI
{
    /// <summary>Permite arrastrar una ventana por su barra de título.</summary>
    public sealed class UiDragHandle : MonoBehaviour, IDragHandler, IBeginDragHandler
    {
        public RectTransform Target;

        public void OnBeginDrag(PointerEventData eventData) => Target.SetAsLastSibling();

        public void OnDrag(PointerEventData eventData)
        {
            var canvas = Target.GetComponentInParent<Canvas>();
            float scale = canvas != null ? canvas.scaleFactor : 1f;
            Target.anchoredPosition += eventData.delta / scale;
        }
    }
}
