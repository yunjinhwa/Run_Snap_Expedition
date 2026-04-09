using UnityEngine;

public class SwipeInput : MonoBehaviour
{
    [SerializeField] private float minSwipeDistance = 100f;
    [SerializeField] private CharactorController characterController;

    private Vector2 touchStartPos;
    private Vector2 touchEndPos;
    private bool isSwiping = false;

    private void Update()
    {
#if UNITY_EDITOR
        HandleMouseInput();
#else
        HandleTouchInput();
#endif
    }

    private void HandleTouchInput()
    {
        if (Input.touchCount == 0)
            return;

        Touch touch = Input.GetTouch(0);

        switch (touch.phase)
        {
            case TouchPhase.Began:
                touchStartPos = touch.position;
                isSwiping = true;
                break;

            case TouchPhase.Ended:
            case TouchPhase.Canceled:
                if (!isSwiping)
                    return;

                touchEndPos = touch.position;
                CheckSwipe();
                isSwiping = false;
                break;
        }
    }

    private void HandleMouseInput()
    {
        if (Input.GetMouseButtonDown(0))
        {
            touchStartPos = Input.mousePosition;
            isSwiping = true;
        }

        if (Input.GetMouseButtonUp(0))
        {
            if (!isSwiping)
                return;

            touchEndPos = Input.mousePosition;
            CheckSwipe();
            isSwiping = false;
        }
    }

    private void CheckSwipe()
    {
        Vector2 delta = touchEndPos - touchStartPos;

        if (delta.magnitude < minSwipeDistance)
            return;

        if (Mathf.Abs(delta.y) > Mathf.Abs(delta.x) && delta.y > 0)
        {
            characterController.Jump();
        }
    }
}