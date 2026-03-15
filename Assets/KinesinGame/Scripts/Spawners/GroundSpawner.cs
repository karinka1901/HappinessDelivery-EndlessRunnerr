using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    public GameObject groundTilePrefab;
    private Vector3 nextSpawnPoint;

    public bool canSpawnObstacles; 


    public void Start()
    {
        if( groundTilePrefab == null)
        {
            Debug.LogError("Ground tile prefab is not assigned in the inspector.");
            return;
        }

        for (int i = 0; i < 2; i++)
        {
            SpawnTile();
        }
    }

    public void SpawnTile()
    {

        for (int i = 0; i < 2; i++) //spawn two tiles at a time 
        {
            GameObject tile = Instantiate(groundTilePrefab, nextSpawnPoint, Quaternion.identity);
            nextSpawnPoint = tile.transform.GetChild(0).transform.position;
        }

    }
}
