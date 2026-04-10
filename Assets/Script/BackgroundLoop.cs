using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    [Header("Background Settings")]
    [SerializeField] private Transform[] backgrounds;
    [SerializeField] private float moveSpeed = 3f;
    [SerializeField] private float backgroundWidth = 20f;

    [Header("Distance Settings")]
    [SerializeField] private float distanceMultiplier = 1f;

    private Camera mainCamera;
    private float cameraLeftEdge;

    private float distanceAccumulator = 0f;

    private void Start()
    {
        mainCamera = Camera.main;
    }

    private void Update()
    {
        MoveBackgrounds();
        RepositionBackgrounds();
    }

    private void MoveBackgrounds()
    {
        float moveAmount = moveSpeed * Time.deltaTime;

        for (int i = 0; i < backgrounds.Length; i++)
        {
            backgrounds[i].position += Vector3.left * moveAmount;
        }

        AddMovedDistance(moveAmount);
    }

    private void AddMovedDistance(float moveAmount)
    {
        distanceAccumulator += moveAmount * distanceMultiplier;

        int distanceToAdd = Mathf.FloorToInt(distanceAccumulator);

        if (distanceToAdd > 0)
        {
            StateManager.Instance.AddDistance(distanceToAdd);
            distanceAccumulator -= distanceToAdd;
        }
    }

    private void RepositionBackgrounds()
    {
        cameraLeftEdge = mainCamera.transform.position.x - GetCameraHalfWidth();

        for (int i = 0; i < backgrounds.Length; i++)
        {
            float bgRightEdge = backgrounds[i].position.x + (backgroundWidth / 2f);

            if (bgRightEdge < cameraLeftEdge)
            {
                Transform rightMost = GetRightMostBackground();
                float newX = rightMost.position.x + backgroundWidth;

                backgrounds[i].position = new Vector3(
                    newX,
                    backgrounds[i].position.y,
                    backgrounds[i].position.z
                );
            }
        }
    }

    private Transform GetRightMostBackground()
    {
        Transform rightMost = backgrounds[0];

        for (int i = 1; i < backgrounds.Length; i++)
        {
            if (backgrounds[i].position.x > rightMost.position.x)
            {
                rightMost = backgrounds[i];
            }
        }

        return rightMost;
    }

    private float GetCameraHalfWidth()
    {
        return mainCamera.orthographicSize * mainCamera.aspect;
    }
}