using UnityEngine;
using UnityEngine.EventSystems;

public class ClickEvent : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("Pointer Down");
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        if (eventData.pointerEnter != gameObject)
        {
            return;
        }
        
        switch(gameObject.tag)
        {
            case "stage1":
                StateManager.Instance.AddScore(100);
                break;
            case "stage2":
                StateManager.Instance.AddScore(150);
                break;
            case "stage3":
                StateManager.Instance.AddScore(200);
                break;
            case "special":
                StateManager.Instance.AddScore(500);
                break;
        }
        Debug.Log("Pointer Up");
        Debug.Log("Clicked on: " + gameObject.name);
        Debug.Log("Current Score: " + StateManager.Instance.Score);
    }
}
