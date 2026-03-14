using UnityEngine;

public class CollectibleOrb : MonoBehaviour
{
    [SerializeField] private HappinessSystem happinessSystem;
    [SerializeField] private float happinessValue = 20f;
    void Start()
    {
        happinessSystem = FindAnyObjectByType<HappinessSystem>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        if (happinessSystem != null)
        {
            happinessSystem.AddHappiness(happinessValue);
        }

        Destroy(gameObject);
    }
}
