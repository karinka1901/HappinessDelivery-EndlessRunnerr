using UnityEngine;

public class PlayerCollisionDetection : MonoBehaviour
{
    public bool obstacleCollision;
    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Obstacle"))
       {
           DebugColor.Log("Player collided with an obstacle!", "cyan");
            obstacleCollision = true;

        }
    }
}
