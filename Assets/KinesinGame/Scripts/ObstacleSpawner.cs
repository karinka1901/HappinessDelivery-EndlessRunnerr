using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
  public GameObject obstaclePrefab;
   private bool CanSpawnObstacles => GetComponentInParent<GroundTile>().groundSpawner.canSpawnObstacles;
  [SerializeField] private Transform[] spawnPoints;

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
        if (obstaclePrefab == null)
        {
            Debug.LogError("Obstacle prefab is not assigned in the inspector.");
        }
        #endregion

    }
    private void Start()
    {
        SpawnObstacle();
    }

    public void SpawnObstacle()
    {
        if (!CanSpawnObstacles) return; //don't spawn immediately

        int randomPoint = Random.Range(0, spawnPoints.Length);
        GameObject obstacle = Instantiate(obstaclePrefab, spawnPoints[randomPoint].position, Quaternion.identity);
        obstacle.transform.SetParent(this.gameObject.transform); 

    }
}
