using UnityEngine;

public class SpeedLaneSpawner : MonoBehaviour
{
    [SerializeField] private GameObject speedLanePrefab;
    [SerializeField] private Transform[] spawnPoints;

    private void Awake()
    {
        if (spawnPoints.Length == 0)
        {
            spawnPoints = new Transform[transform.childCount];
            for (int i = 0; i < spawnPoints.Length; i++) spawnPoints[i] = transform.GetChild(i);
        }
    }

    private void Start()
    {
        if (speedLanePrefab != null && spawnPoints.Length > 0)
        {
            if(Random.value < 0.8f) 
            {
                SpawnSpeedLane();
            }
        }
    }

    public void SpawnSpeedLane()
    {
        int randomPoint = Random.Range(0, spawnPoints.Length);
        GameObject spawnedSpeedLane = Instantiate(speedLanePrefab, spawnPoints[randomPoint].position, Quaternion.identity);
        spawnedSpeedLane.transform.SetParent(this.gameObject.transform);

    }

    
}
