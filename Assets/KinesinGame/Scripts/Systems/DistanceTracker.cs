using UnityEngine;

public class DistanceTracker : MonoBehaviour
{
    [SerializeField] private Transform player;
    private float startPosition;

    private void Start()
    {
        if (player != null)
            startPosition = player.position.z;
    }

    private void Update()
    {
        if (player == null || GameManager.Instance == null || GameManager.Instance.IsGameOver)
            return;

        float distance = Mathf.Max(0f, player.position.z - startPosition); 
        GameManager.Instance.SetDistance(distance);
    }

   
}