using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShootingJoystick : MonoBehaviour, IDragHandler, IPointerDownHandler, IPointerUpHandler
{
    public Vector2 shotJoystickVec2 { get; private set; }

    public RectTransform joystickRect;
    public Image innerPad;


    void Start()
    {
        shotJoystickVec2 = new Vector2(0, 0);
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

            shotJoystickVec2 = new Vector2(inputX, inputY);

            if (shotJoystickVec2.magnitude > 1.0f)
                shotJoystickVec2 = shotJoystickVec2.normalized;

            // Draw InnerPad
            // range : (-100, -100) ~ (100, 100)
            innerPad.rectTransform.anchoredPosition
                = new Vector2(shotJoystickVec2.x * radiusX, shotJoystickVec2.y * radiusY);
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        innerPad.rectTransform.anchoredPosition = Vector2.zero;
        shotJoystickVec2 = Vector2.zero;
    }
}
