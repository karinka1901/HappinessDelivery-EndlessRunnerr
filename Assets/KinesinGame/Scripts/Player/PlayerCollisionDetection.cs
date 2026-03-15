using UnityEngine;

[RequireComponent(typeof(PlayerController))]
public class PlayerCollisionDetection : MonoBehaviour
{
    public bool obstacleCollision;
    private PlayerController playerController => GetComponent<PlayerController>();

    private void OnTriggerEnter(Collider other)
    {
       if (other.CompareTag("Obstacle") && !playerController.isInvulnerable)
       {
           DebugColor.Log("Player collided with an obstacle!", "cyan");
            if(GameManager.Instance != null && GameManager.Instance.happinessSystem != null)
            {
                GameManager.Instance.happinessSystem.RemoveHappiness(20f);
                playerController.Hurt();
                
            }
            obstacleCollision = true;

        }
       if (other.CompareTag("SpeedLane"))
       {
           playerController.speed *= 3f; // Triple the speed when entering a speed lane
       }
       if (other.CompareTag("Death"))
        {
            DebugColor.Log("Player hit a death zone!", "red");
            playerController.Die();
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
