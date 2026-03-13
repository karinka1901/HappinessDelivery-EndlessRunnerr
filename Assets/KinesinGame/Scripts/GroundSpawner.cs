using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTilePrefab;
    private Vector3 nextSpawnPoint;

    public void SpawnTile()
    {
        GameObject tile = Instantiate(groundTilePrefab, nextSpawnPoint, Quaternion.identity);
        nextSpawnPoint = tile.transform.GetChild(1).transform.position;
    }

    public void Start()
    {
        for (int i = 0; i < 10; i++)
        {
            SpawnTile();
        }
    }
}
