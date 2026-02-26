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

    [Header("Collectibles")]
    [SerializeField] private GameObject coinPrefab;
    [SerializeField] private float coinSpawnChance = 0.5f;  // 50% chance
    [SerializeField] private float coinHeight = -2f;  // Height above ground

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

            if (Random.value < coinSpawnChance)
            {
                SpawnCoin();
            }

            spawnInterval = Mathf.Max(minSpawnInterval, spawnInterval - spawnIntervalDecrease);
            nextSpawnTime = Time.time + spawnInterval;
        }

        CleanupObstackles();
        CleanupCoins();
    }

    private void SpawnCoin()
    {
        if (player == null || coinPrefab == null) return;

        Vector3 spawnPosition = new Vector3(player.position.x + spawnDistance, coinHeight, 0);

        Instantiate(coinPrefab, spawnPosition, Quaternion.identity);
    }

    void CleanupCoins()
    {
        if (player == null) return;
        
        GameObject[] coins = GameObject.FindGameObjectsWithTag("collectibles");
        
        foreach (GameObject coin in coins)
        {
            if (coin.transform.position.x < player.position.x - despawnDistance)
            {
                Destroy(coin);
            }
        }
    }

    void SpawnObstackle()
    {
        if (player == null) return;  // Safety check
        
        // Calculate spawn position AHEAD of player ← CHANGED!
        Vector3 spawnPosition = new(
            player.transform.position.x + spawnDistance,  // Player X + 15
            spawnYPosition,
            0
        );
        
        int randomIndex = Random.Range(0, obstacklePrefabs.Length);
        GameObject obstacklePrefab = obstacklePrefabs[randomIndex];

        Instantiate(obstacklePrefab, spawnPosition, Quaternion.identity);


    }

    void CleanupObstackles()
    {

        if (player == null) return;
        GameObject[] obstackles = GameObject.FindGameObjectsWithTag("Obstackle");

        foreach (GameObject obstackle in obstackles)
        {
            if (obstackle.transform.position.x < player.position.x - despawnDistance)
            {
                Destroy(obstackle);
            }
        }
    }
}
