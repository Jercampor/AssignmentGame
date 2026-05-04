using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI timerText;
    private int score = 0;
    private float timeRemaining = 120f;
    private bool gameActive = true;

    public GameObject gameOverPanel;
    public GameObject winPanel;
    public TextMeshProUGUI gameOverScoreText;
    public TextMeshProUGUI winScoreText;

    void Awake()
    {
        instance = this;
    }

    void Update()
    {
        if (!gameActive) return;

        timeRemaining -= Time.deltaTime;
        timerText.text = "Time: " + Mathf.CeilToInt(timeRemaining);

        if (timeRemaining <= 0)
        {
            timeRemaining = 0;
            GameOver();
        }
    }

    public void AddScore(int amount)
    {
        score += amount;
        scoreText.text = "Score: " + score;

        if (score >= 500)
        {
            Win();
        }
    }

    public void GameOver()
    {
        gameActive = false;
        Time.timeScale = 0f;
        gameOverPanel.SetActive(true);
        gameOverScoreText.text = "Score: " + score;
    }

    public void Win()
    {
        gameActive = false;
        Time.timeScale = 0f;
        winPanel.SetActive(true);
        winScoreText.text = "Score: " + score;
    }

    public void RestartGame()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void GoToMainMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}