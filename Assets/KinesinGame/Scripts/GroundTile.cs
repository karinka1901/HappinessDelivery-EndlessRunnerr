using Unity.VisualScripting;
using UnityEngine;

public class GroundTile : MonoBehaviour
{
    public GroundSpawner groundSpawner;

    private void Start()
    {
        groundSpawner = FindAnyObjectByType<GroundSpawner>();
    }
    public void OnTriggerExit(Collider other) //when the player exits the tile, spawn a new one and destroy this one after a delay
    {
        if (!other.CompareTag("Player")) return;

        if (groundSpawner != null) 
        {
            groundSpawner.SpawnTile();
            groundSpawner.canSpawnObstacles = true;
            Destroy(gameObject, 2);
        }

    }
}