using UnityEngine;
using UnityEngine.InputSystem;

public class Document : MonoBehaviour
{
    RectTransform rectTransform;
    Canvas canvas;
    Vector2 difference;

    bool isDragging;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        canvas = GetComponentInParent<Canvas>();
    }

    private void Update()
    {
        if (Mouse.current == null) return;

        Vector2 mousePosition = Mouse.current.position.ReadValue();

        RectTransformUtility.ScreenPointToLocalPointInRectangle((RectTransform)canvas.transform,mousePosition,canvas.renderMode == RenderMode.ScreenSpaceOverlay? null : canvas.worldCamera,out Vector2 localMousePosition);

        if (Mouse.current.leftButton.wasPressedThisFrame)
        {
            if (RectTransformUtility.RectangleContainsScreenPoint(rectTransform,mousePosition,canvas.renderMode == RenderMode.ScreenSpaceOverlay? null : canvas.worldCamera))
            {
                difference = (Vector2)rectTransform.localPosition - localMousePosition;
                isDragging = true;
            }
        }

        if (isDragging && Mouse.current.leftButton.isPressed)
        {
            rectTransform.localPosition = localMousePosition + difference;
        }

        if (Mouse.current.leftButton.wasReleasedThisFrame)
        {
            isDragging = false;
        }
    }
}
