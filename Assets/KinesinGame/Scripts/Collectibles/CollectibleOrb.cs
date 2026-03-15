using UnityEngine;

public class CollectibleOrb : MonoBehaviour
{
    [SerializeField] private HappinessSystem happinessSystem;
    private float happinessValue;

    public void Initialize(float value)
    {
        happinessValue = value;
    }

    void Start()
    {
        happinessSystem = FindAnyObjectByType<HappinessSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Obstacle"))
        {
            Destroy(gameObject);
            //DebugColor.Log("Orb destroyed by obstacle", "red");
            return;
        }
        
        if (other.CompareTag("Orb"))
        {
            Destroy(gameObject);
            DebugColor.Log("Orbs were too close", "green");
            return;
        }

        if (other.CompareTag("Player"))
        {
            happinessSystem.AddHappiness(happinessValue);
            DebugColor.Log($"Collected an orb worth {happinessValue} happiness!", "green");
            Destroy(gameObject);
            DebugColor.Log("Orb collected! Happiness increased.", "green");
        }

    }
}
