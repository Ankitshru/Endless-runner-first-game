using UnityEngine;

public class CoinRotate : MonoBehaviour
{
    [SerializeField] private float rotationSpeed = 180f;

    void Update()
    {
        transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
    }
}
