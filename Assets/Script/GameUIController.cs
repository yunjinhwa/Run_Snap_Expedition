using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameUIController : MonoBehaviour
{
    [Header("Distance UI")]
    [SerializeField] private TMP_Text distanceText;

    [Header("Heart UI")]
    [SerializeField] private Image[] hearts;

    [Header("Text Format")]
    [SerializeField] private string distancePrefix = "Distance: ";
    [SerializeField] private string distanceSuffix = "m";

    private void Start()
    {
        RefreshUI();
    }

    private void Update()
    {
        RefreshUI();
    }

    private void RefreshUI()
    {
        UpdateDistanceUI();
        UpdateHeartUI();
    }

    private void UpdateDistanceUI()
    {
        if (distanceText == null)
            return;

        distanceText.text = distancePrefix + StateManager.Instance.TotalDistance + distanceSuffix;
    }

    private void UpdateHeartUI()
    {
        if (hearts == null || hearts.Length == 0)
            return;

        int currentHP = StateManager.Instance.HP;

        for (int i = 0; i < hearts.Length; i++)
        {
            if (hearts[i] == null)
                continue;

            hearts[i].enabled = i < currentHP;
        }
    }
}