using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Obstackle"))
        {
            GameOver.Instance.GameOverNow();
        }
    }


}
