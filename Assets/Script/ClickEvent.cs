using UnityEngine;
using UnityEngine.EventSystems;

public class ClickEvent : MonoBehaviour, IPointerClickHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        switch (gameObject.tag)
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

        Debug.Log("Clicked on: " + gameObject.name);
        Debug.Log("Current Score: " + StateManager.Instance.Score);
        Destroy(gameObject);
    }
}