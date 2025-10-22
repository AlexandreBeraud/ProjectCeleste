using TMPro;
using UnityEngine;

public class PointsSystemController : MonoBehaviour
{
    public static PointsSystemController Instance;

    [Header("UI Elements")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI bestScoreText;

    [Header("DEBUG")]
    [SerializeField] private int actualScore;
    [SerializeField] private int bestScore;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadBestScore();
        UpdateUI();
    }

    public void AddScore(int points)
    {
        actualScore += points;

        if (actualScore > bestScore)
        {
            bestScore = actualScore;
            SaveBestScore();
        }

        UpdateUI();
    }

    public void ResetScore() //use it in menus
    {
        actualScore = 0;
        UpdateUI();
    }

    private void UpdateUI()
    {
        scoreText.text = $"Score : {actualScore}";
        bestScoreText.text = $"Best Score : {bestScore}";
    }
    
    private void SaveBestScore()
    {
        PlayerPrefs.SetInt("BestScore", bestScore);
        Debug.Log($"Best Score saved : {bestScore}");
    }

    private void LoadBestScore()
    {
        bestScore = PlayerPrefs.GetInt("BestScore", 0);
        Debug.Log($"Best Score loaded : {bestScore}");
    }
}