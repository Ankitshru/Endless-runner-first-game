using UnityEngine;
using TMPro;
using UnityEngine.PlayerLoop;
using System;
public class ScoreManager : MonoBehaviour
{
    public static ScoreManager Instance;

    [Header("Score Settings")]
    [SerializeField] private float scoreMultiplier = 10f;

    [Header("UI Reference")]
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highScoreText;
    [SerializeField] private TextMeshProUGUI coinCountText;

    private float score = 0f;
    private float highScore = 0f;
    private int coinsCollected = 0;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        highScore = PlayerPrefs.GetFloat("HighScore", 0f);
        UpdateUI();
    }

    void Update()
    {
        if (GameOver.Instance != null && GameOver.Instance.isGameOver)
            return;
        
        score += scoreMultiplier * Time.deltaTime;

        if (score > highScore)
        {
            highScore = score;
            PlayerPrefs.SetFloat("HighScore", highScore);
        }


        UpdateUI();
    }

    private void UpdateUI()
    {
        if (scoreText != null)
        {
            scoreText.text = "Score: " + Mathf.FloorToInt(score).ToString();           
        }
        if (highScoreText != null)
        {
            highScoreText.text =    "Best: " + Mathf.FloorToInt(highScore).ToString();
        }
        if (coinCountText != null)
        {
            coinCountText.text = "Coins: " +  coinsCollected.ToString();
        }
    }

    public void AddCoinScore(int coinValue)
    {
        score += coinValue;
        coinsCollected++;
        UpdateUI();
    }

    public void ResetScore()
    {
        score = 0f;
        coinsCollected = 0;
        UpdateUI();
    }

    
}
