using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;
    
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 1f;
    
    void Awake()
    {
        // Singleton pattern
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);  // Persists between scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
    
    void Start()
    {
        // Fade in when scene starts
        if (fadeImage != null)
        {
            StartCoroutine(FadeIn());
        }
    }
    
    public void LoadScene(string sceneName)
    {
        StartCoroutine(FadeOutAndLoad(sceneName));
    }
    
    public void LoadScene(int sceneIndex)
    {
        StartCoroutine(FadeOutAndLoad(sceneIndex));
    }
    
    IEnumerator FadeIn()
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        
        color.a = 0f;
        fadeImage.color = color;
    }
    
    IEnumerator FadeOutAndLoad(string sceneName)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        
        SceneManager.LoadScene(sceneName);
    }
    
    IEnumerator FadeOutAndLoad(int sceneIndex)
    {
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        
        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.unscaledDeltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        
        SceneManager.LoadScene(sceneIndex);
    }
}