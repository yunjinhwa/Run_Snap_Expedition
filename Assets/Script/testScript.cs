using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

public class testScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if(eventData.pointerEnter != gameObject)
        {
            return;
        }
        Debug.Log("Pointer Up");
    }
}
