using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class SceneTransition : MonoBehaviour
{
    public static SceneTransition Instance;
    
    [SerializeField] private Image fadeImage;
    [SerializeField] private float fadeDuration = 0.5f;
    
    private Canvas fadeCanvas;
    private bool isTransitioning = false;
    
    void Awake()
    {
        // Singleton - persist across scenes
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            
            // Also make the canvas persist
            if (fadeImage != null && fadeImage.canvas != null)
            {
                fadeCanvas = fadeImage.canvas;
                DontDestroyOnLoad(fadeCanvas.gameObject);
            }
        }
        else
        {
            Destroy(gameObject);
            return;
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
    
    void OnEnable()
    {
        // Subscribe to scene loaded event
        SceneManager.sceneLoaded += OnSceneLoaded;
    }
    
    void OnDisable()
    {
        // Unsubscribe
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
    
    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        // Fade in when new scene loads
        if (fadeImage != null && !isTransitioning)
        {
            StartCoroutine(FadeIn());
        }
    }
    
    public void LoadScene(string sceneName)
    {
        if (!isTransitioning)
        {
            StartCoroutine(TransitionToScene(sceneName));
        }
    }
    
    public void LoadScene(int sceneIndex)
    {
        if (!isTransitioning)
        {
            StartCoroutine(TransitionToScene(sceneIndex));
        }
    }
    
    IEnumerator TransitionToScene(string sceneName)
    {
        isTransitioning = true;
        
        // Fade out
        yield return StartCoroutine(FadeOut());
        
        // Load scene
        SceneManager.LoadScene(sceneName);
        
        isTransitioning = false;
    }
    
    IEnumerator TransitionToScene(int sceneIndex)
    {
        isTransitioning = true;
        
        // Fade out
        yield return StartCoroutine(FadeOut());
        
        // Load scene
        SceneManager.LoadScene(sceneIndex);
        
        isTransitioning = false;
    }
    
    IEnumerator FadeIn()
    {
        if (fadeImage == null) yield break;
        
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        
        while (elapsedTime < fadeDuration)
        {
            if (fadeImage == null) yield break;  // Safety check
            
            elapsedTime += Time.unscaledDeltaTime;
            color.a = Mathf.Lerp(1f, 0f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        
        if (fadeImage != null)
        {
            color.a = 0f;
            fadeImage.color = color;
        }
    }
    
    IEnumerator FadeOut()
    {
        if (fadeImage == null) yield break;
        
        float elapsedTime = 0f;
        Color color = fadeImage.color;
        
        while (elapsedTime < fadeDuration)
        {
            if (fadeImage == null) yield break;  // Safety check
            
            elapsedTime += Time.unscaledDeltaTime;
            color.a = Mathf.Lerp(0f, 1f, elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }
        
        if (fadeImage != null)
        {
            color.a = 1f;
            fadeImage.color = color;
        }
    }
}