using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    [Header("Player Settings")]
    [SerializeField] private int startHP = 3;
    [SerializeField] private int maxHP = 3;

    [Header("Scene Names")]
    [SerializeField] private string stage1SceneName = "Stage1Scene";
    [SerializeField] private string stage2SceneName = "Stage2Scene";
    [SerializeField] private string stage3SceneName = "Stage3Scene";
    [SerializeField] private string clearSceneName = "ClearScene";
    [SerializeField] private string gameOverSceneName = "GameOverScene";
    [SerializeField] private string titleSceneName = "TitleScene";

    [Header("Stage Clear Distance")]
    [SerializeField] private int stage1ClearDistance = 200;
    [SerializeField] private int stage2ClearDistance = 300;
    [SerializeField] private int stage3ClearDistance = 400;

    public bool IsGameOver { get; private set; }
    public bool IsGameClear { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    private void OnDisable()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void Update()
    {
        if (!IsPlayableStage())
            return;

        if (IsGameOver || IsGameClear)
            return;

        CheckGameOver();
        CheckStageClear();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Time.timeScale = 1f;
        IsGameOver = false;
        IsGameClear = false;

        // 새 게임 시작은 Stage1 진입 시점에만 전체 초기화
        if (scene.name == stage1SceneName)
        {
            StateManager.Instance.Reset(startHP);
        }
    }

    private bool IsPlayableStage()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        return currentSceneName == stage1SceneName ||
               currentSceneName == stage2SceneName ||
               currentSceneName == stage3SceneName;
    }

    private void CheckGameOver()
    {
        if (StateManager.Instance.HP <= 0)
        {
            GameOver();
        }
    }

    private void CheckStageClear()
    {
        int targetDistance = GetCurrentStageClearDistance();

        if (targetDistance <= 0)
            return;

        if (StateManager.Instance.TotalDistance >= targetDistance)
        {
            ClearStage();
        }
    }

    private int GetCurrentStageClearDistance()
    {
        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == stage1SceneName)
            return stage1ClearDistance;

        if (currentSceneName == stage2SceneName)
            return stage2ClearDistance;

        if (currentSceneName == stage3SceneName)
            return stage3ClearDistance;

        return -1;
    }

    public void GameOver()
    {
        if (IsGameOver)
            return;

        IsGameOver = true;
        Time.timeScale = 1f;
        SceneManager.LoadScene(gameOverSceneName);
    }

    public void ClearStage()
    {
        if (IsGameClear)
            return;

        IsGameClear = true;
        Time.timeScale = 1f;

        string currentSceneName = SceneManager.GetActiveScene().name;

        if (currentSceneName == stage1SceneName)
        {
            PrepareNextStage();
            SceneManager.LoadScene(stage2SceneName);
        }
        else if (currentSceneName == stage2SceneName)
        {
            PrepareNextStage();
            SceneManager.LoadScene(stage3SceneName);
        }
        else if (currentSceneName == stage3SceneName)
        {
            PrepareNextStage();
            StateManager.Instance.AddScore(StateManager.Instance.HP * 200);
            SceneManager.LoadScene(clearSceneName);
        }
    }

    private void PrepareNextStage()
    {
        // 스테이지 클리어 보상: HP +1, 최대 3
        int nextHP = Mathf.Min(StateManager.Instance.HP + 1, maxHP);
        StateManager.Instance.SetHP(nextHP);
        StateManager.Instance.AddScore(500); // 스테이지 클리어 보상 점수
        if (StateManager.Instance.HitCount == 0)
        {
            StateManager.Instance.AddScore(300); // 무피격 보너스 점수
        }

        // 다음 스테이지에서는 거리만 다시 0부터
        StateManager.Instance.SetTotalDistance(0);
        StateManager.Instance.SetHitCount(0);
    }

    public void LoadStage1()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(stage1SceneName);
    }

    public void RestartCurrentStage()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void LoadTitleScene()
    {
        Time.timeScale = 1f;
        IsGameOver = false;
        IsGameClear = false;

        StateManager.Instance.Reset(startHP);
        StateManager.Instance.SetHitCount(0);

        SceneManager.LoadScene(titleSceneName);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}