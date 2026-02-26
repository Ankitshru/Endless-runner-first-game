using UnityEngine;

public class CollisionDetection : MonoBehaviour
{

    [Header("Audio")]
    [SerializeField] private AudioClip coinSound;
    [SerializeField] private AudioClip deathSound;

    [Header("Effects")]
    [SerializeField] private GameObject deathExplosionPrefab;
    private AudioSource audioSource;
    
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstackle"))
        {
            GameOver.Instance.GameOverNow();
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Obstackle"))
        {
            GameOver.Instance.GameOverNow();
            Debug.Log("Fell in pit!");
        }

        if (other.CompareTag("collectibles"))
        {
            CollectCoin(other.gameObject);
        }
    }

    void Die()
    {
        if (deathExplosionPrefab != null)
        {
            Instantiate(deathExplosionPrefab, transform.position, Quaternion.identity);
        }

        if (audioSource != null && deathSound != null)
        {
            audioSource.PlayOneShot(deathSound);
        }
        GameOver.Instance.GameOverNow();
        
    }

    void CollectCoin(GameObject coin)
    {
        if (ScoreManager.Instance != null)
            ScoreManager.Instance.AddCoinScore(50);

        if (audioSource != null && coinSound != null)
            audioSource.PlayOneShot(coinSound);

        Destroy(coin);
    } 

}
