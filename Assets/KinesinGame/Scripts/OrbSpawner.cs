using System.Collections.Generic;
using UnityEngine;

public class OrbSpawner : MonoBehaviour
{
    [SerializeField] private GameObject orbPrefab;
    
    [SerializeField] private float spawnInterval = 5f;

    [SerializeField] private Transform[] spawnPoints;
    public ObstacleSpawner obstacleSpawner;

    private void Start()
    {
       // obstacleSpawner = GetComponentInParent<ObstacleSpawner>();
        SpawnOrb(obstacleSpawner.SpawnedLane); // Spawn an orb immediately when the tile is created
    }

    private void SpawnOrb(int blockedLane)
    {
        if (spawnPoints.Length == 0 || orbPrefab == null) return;

        List<int> availableLanes = new();

        for (int i = 0; i < spawnPoints.Length; i++)
        {
            if (i != blockedLane)
            {
                availableLanes.Add(i);
            }
        }
        if (availableLanes.Count == 0)
            return;

        int chosenLane = availableLanes[Random.Range(0, availableLanes.Count)];
        Vector3 spawnPosition = spawnPoints[chosenLane].position;

        GameObject orb = Instantiate(orbPrefab, spawnPosition, Quaternion.identity);
        orb.transform.SetParent(transform);
    }
}