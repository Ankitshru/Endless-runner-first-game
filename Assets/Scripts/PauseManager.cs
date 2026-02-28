using System;
using JetBrains.Annotations;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public static PauseManager Instance; 

    [Header("UI Reference")]
    [SerializeField] private GameObject pausePanel;

    [Header("Audio")]
    [SerializeField] private AudioClip pauseSound;
    [SerializeField] private AudioClip unpauseSound;

    private bool isPaused = false;
    private AudioSource audioSource;

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
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (isPaused)
            {
                ResumeGame();
            }
            else
            {
                PauseGame();
            }
        }
    }

    public void PauseGame()
    {
        if (GameOver.Instance != null && GameOver.Instance.isGameOver)
            return;
        
        isPaused = true;

        if (pausePanel != null)
        {
            pausePanel.SetActive(true);
        }

        Time.timeScale = 0f;

        if (audioSource != null && pauseSound != null)
        {
            audioSource.PlayOneShot(pauseSound);
        }
    }

    public void ResumeGame()
    {
        isPaused = false;

        if (pausePanel != null)
        {
            pausePanel.SetActive(false);
        }

        Time.timeScale = 1f;

        if (audioSource != null && unpauseSound != null)
        {
            audioSource.PlayOneShot(unpauseSound);
        }
    }

    public void PauseAndReturnToMenu()
    {
        isPaused = false;
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }

    public void PauseAndQuit()
    {
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false; 
        #else
            Application.Quit()
        #endif
    }

    public bool IsPaused()
    {
        return isPaused;
    }
}
