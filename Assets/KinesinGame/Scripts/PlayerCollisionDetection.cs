using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerCollisionDetection : MonoBehaviour
{
    public bool obstacleCollision;
    private PlayerController playerController => GetComponent<PlayerController>();

    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Obstacle"))
       {
           DebugColor.Log("Player collided with an obstacle!", "cyan");
            obstacleCollision = true;

        }
       if (other.CompareTag("SpeedLane"))
       {
           playerController.speed *= 3f; // Triple the speed when entering a speed lane
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("SpeedLane"))
        {
            playerController.speed /= 3f; // Reset speed to normal
        }
    }
}
