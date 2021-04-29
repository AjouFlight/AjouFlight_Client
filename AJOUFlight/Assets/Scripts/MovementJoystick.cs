using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MovementJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Vector2 moveJoystickVec2 { get; private set; }

    public RectTransform joystickRect;
    public Image innerPad;


    void Start()
    {
        moveJoystickVec2 = new Vector2(0, 0);
    }


    public void OnDrag(PointerEventData eventData)
    {
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(joystickRect,
            eventData.position, eventData.pressEventCamera, out Vector2 localPoint))
        {
            float radiusX = joystickRect.sizeDelta.x / 2;
            float radiusY = joystickRect.sizeDelta.y / 2;

            // input range : -1 ~ 1
            float inputX = (localPoint.x / radiusX) - 1;
            float inputY = (localPoint.y / radiusY) - 1;

            moveJoystickVec2 = new Vector2(inputX, inputY);

            if (moveJoystickVec2.magnitude > 1.0f)
                moveJoystickVec2 = moveJoystickVec2.normalized;

            // Draw InnerPad
            // range : (-100, -100) ~ (100, 100)
            innerPad.rectTransform.anchoredPosition
                = new Vector2(moveJoystickVec2.x * radiusX, moveJoystickVec2.y * radiusY);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        innerPad.rectTransform.anchoredPosition = Vector2.zero;
        moveJoystickVec2 = Vector2.zero;
    }
}