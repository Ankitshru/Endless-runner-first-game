using UnityEngine;
using UnityEngine.EventSystems;

public class ButtonHoverEffect : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] private float hoverScale = 1.1f;
    [SerializeField] private float scaleSpeed = 10f;
    
    private Vector3 originalScale;
    private Vector3 targetScale;
    
    void Start()
    {
        originalScale = transform.localScale;
        targetScale = originalScale;
    }
    
    void Update()
    {
        // Smoothly scale to target
        transform.localScale = Vector3.Lerp(transform.localScale, targetScale, Time.unscaledDeltaTime * scaleSpeed);
    }
    
    public void OnPointerEnter(PointerEventData eventData)
    {
        // Mouse entered button - scale up
        targetScale = originalScale * hoverScale;
    }
    
    public void OnPointerExit(PointerEventData eventData)
    {
        // Mouse left button - return to normal
        targetScale = originalScale;
    }
}