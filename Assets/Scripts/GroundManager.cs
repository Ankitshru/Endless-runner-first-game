using System;
using UnityEngine;

public class GroundManager : MonoBehaviour
{
    [Header("Ground Settings")]
    [SerializeField] private GameObject groundSegmentPrefab;
    [SerializeField] private int numberOfSegments = 3; 
    [SerializeField] private float segmentLength = 20f;
    [SerializeField] private float groundY = -4.5f;

    [Header("References")]
    [SerializeField] private Transform player;

    private GameObject[] groundSegments;
    private float rightMostSegmentsX;


    void Start()
    {
        if (player == null)
        {
            GameObject playerObj = GameObject.FindGameObjectWithTag("Player");
            if (playerObj != null)
            {
                player = playerObj.transform;
            }

        }

        groundSegments = new GameObject[numberOfSegments];

        for (int i = 0; i < numberOfSegments; i++)
        {
            SpawnGroundSegment(i);
        }
        
        rightMostSegmentsX = (numberOfSegments - 1) * segmentLength;
    }

    void Update()
    {
        if (player == null) return;

        if (player.position.x > rightMostSegmentsX - (numberOfSegments * segmentLength / 2))
        {
            MoveLeftMostSegmentToRight();
        }
    }

    private void SpawnGroundSegment(int index)
    {
        Vector3 spawnPosition = new Vector3(index * segmentLength, groundY, 0);
        groundSegments[index] = Instantiate(groundSegmentPrefab, spawnPosition, Quaternion.identity);
        groundSegments[index].transform.parent = transform;
        groundSegments[index].name = "GroundSegment_" + index;
    }

    private void MoveLeftMostSegmentToRight()
    {
        GameObject leftMost = groundSegments[0];
        float leftMostX = leftMost.transform.position.x;

        float rightMostX = rightMostSegmentsX;

        leftMost.transform.position = new Vector3(rightMostX + segmentLength, groundY, 0);

        rightMostSegmentsX += segmentLength;

        GameObject temp = groundSegments[0];

        for (int i = 0; i < numberOfSegments - 1; i++)
        {
            groundSegments[i] = groundSegments[i + 1];
        }
        groundSegments[numberOfSegments - 1] = temp;  
        
    }
}
