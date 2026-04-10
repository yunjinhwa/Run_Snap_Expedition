using TMPro;
using UnityEngine;

public class ResultUIController : MonoBehaviour
{
    [Header("Score UI")]
    [SerializeField] private TMP_Text scoreText;

    [Header("Text Format")]
    [SerializeField] private string scorePrefix = "Score : ";

    private void Start()
    {
        RefreshUI();
    }

    private void RefreshUI()
    {
        if (scoreText != null)
        {
            scoreText.text = scorePrefix + StateManager.Instance.Score;
        }
    }
}