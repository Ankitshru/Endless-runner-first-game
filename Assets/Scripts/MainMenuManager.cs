using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip buttonClickSound;
    
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
        
        Time.timeScale = 1f;
    }
    
    public void PlayGame()
    {
        Debug.Log("Play button clicked!");
        
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
        
        // Use transition if available ← CHANGED!
        if (SceneTransition.Instance != null)
        {
            SceneTransition.Instance.LoadScene("GameScene");
        }
        else
        {
            SceneManager.LoadScene("GameScene");
        }
    }
    
    public void QuitGame()
    {
        Debug.Log("Quit button clicked!");
        
        if (audioSource != null && buttonClickSound != null)
        {
            audioSource.PlayOneShot(buttonClickSound);
        }
        
        #if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
        #else
            Application.Quit();
        #endif
    }

    public void ReturnToMenu()
{
    Time.timeScale = 1f;  // Unpause
    
    if (SceneTransition.Instance != null)
    {
        SceneTransition.Instance.LoadScene("MainMenu");
    }
    else
    {
        SceneManager.LoadScene("MainMenu");
    }
}
}