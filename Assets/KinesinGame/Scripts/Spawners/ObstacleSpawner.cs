using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [Header("Obstacle Prefabs")]
    [SerializeField]private GameObject groundObstaclePrefab;
    [SerializeField]private GameObject airObstaclePrefab;

    [Header("Spawn Settings")]
    [SerializeField] private Transform[] spawnPoints;
    private float airObstacleHeight = 1.2f;

    [Header("Properties")]
    private GameObject[] ObstaclePrefabs => new GameObject[] { groundObstaclePrefab, airObstaclePrefab };
    private bool CanSpawnObstacles => GetComponentInParent<GroundTile>().groundSpawner.canSpawnObstacles;
    public int SpawnedLane { get; private set; } //lane of the last spawned obstacle

    [Header("Runtime Variables")]
    private int randomPoint;
    private Vector3 spawnPosition;
    private GameObject prefabToSpawn;

    private void Awake()
    {
        #region null checks
        if (spawnPoints.Length == 0)
        {
            spawnPoints = new Transform[transform.childCount];

            for (int i = 0; i < transform.childCount; i++)
            {
                spawnPoints[i] = transform.GetChild(i);
            }
        }
        if (groundObstaclePrefab == null)
        {
            Debug.LogError("Obstacle prefab is not assigned in the inspector.");
            return;
        }
        if (airObstaclePrefab == null)
        {
            Debug.LogError("Air obstacle prefab is not assigned in the inspector.");
            return;
        }
        #endregion
    }

    private void Start()
    {
        SpawnObstacle();
    }

    public void SpawnObstacle() //spawn either a ground or air obstacle at a random spawn point
    {
        if (!CanSpawnObstacles) return; //don't spawn immediately
        if(groundObstaclePrefab == null || airObstaclePrefab == null) {
            Debug.LogError("One or more obstacle prefabs are not assigned in the inspector.");
            return;
        }
        
        randomPoint = Random.Range(0, spawnPoints.Length);
        spawnPosition = spawnPoints[randomPoint].position;
        prefabToSpawn = ObstaclePrefabs[Random.Range(0, ObstaclePrefabs.Length)];

        if (prefabToSpawn == airObstaclePrefab) spawnPosition += Vector3.up * airObstacleHeight;

        GameObject obstacle = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        obstacle.transform.SetParent(transform);
        
        SpawnedLane = randomPoint;
    }
}
