//using System;
using UnityEngine;

public class ObstackleSpawner : MonoBehaviour
{
    [Header("Obstackle Prefabs")]
    [SerializeField] private GameObject[] obstacklePrefabs;

    [Header("Spawn Settings")]
    [SerializeField] private float spawnInterval = 2f;
    [SerializeField] private float minSpawnInterval = 1f;
    [SerializeField] private float spawnIntervalDecrease = 0.05f;
    //[SerializeField] private Vector3 spawnPosition = new Vector3(15, -3.5f, 0);
    [SerializeField] private float spawnDistance = 15f;  // Distance ahead of player ← CHANGED!
    [SerializeField] private float spawnYPosition = -3.5f;

    [Header("References")]
    [SerializeField] private Transform player;

    [Header("CleanUp")]
    //[SerializeField] private float despawnX = -10f;
    [SerializeField] private float despawnDistance = 20f;

    private float nextSpawnTime;

    void Start()
    {
        nextSpawnTime = Time.time + spawnInterval;
    }

    void Update()
    {
        if (GameOver.Instance != null && GameOver.Instance.isGameOver)
            return;

        if (Time.time >= nextSpawnTime)
        {
            SpawnObstackle();

            spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - spawnIntervalDecrease);
            nextSpawnTime = Time.time + spawnInterval;
        }

        CleanupObstackles();
    }

    private void SpawnObstackle()
    {
         if (player == null) return;  // Safety check
        
        // Calculate spawn position AHEAD of player ← CHANGED!
        Vector3 spawnPosition = new Vector3(player.position.x + spawnDistance, spawnYPosition, 0);
        int randomIndex = Random.Range(0, obstacklePrefabs.Length);
        GameObject obstacklePrefab = obstacklePrefabs[randomIndex];

        Instantiate(obstacklePrefab, spawnPosition, Quaternion.identity);


    }

    void CleanupObstackles()
    {

        if (player == null) return;
        GameObject[] obstackles = GameObject.FindGameObjectsWithTag("obstackle");

        foreach (GameObject obstackle in obstackles)
        {
            if (obstackle.transform.position.x < player.position.x - despawnDistance)
            {
                Destroy(obstackle);
            }
        }
    }
}
