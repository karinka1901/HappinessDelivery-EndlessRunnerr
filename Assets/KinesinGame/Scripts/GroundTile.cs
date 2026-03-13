using UnityEngine;

public class GroundTile : MonoBehaviour
{
   [SerializeField] private GroundSpawner groundSpawner;
    private void Start()
    {
        groundSpawner = FindObjectOfType<GroundSpawner>();
    }
    public void OnTriggerExit(Collider other)
    {
        groundSpawner.SpawnTile();
        Destroy(gameObject, 2); 
    }
}
