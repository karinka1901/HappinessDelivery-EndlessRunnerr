using UnityEngine;
using UnityEngine.UI;

public class HappinessSystem : MonoBehaviour
{
    [SerializeField]private float maxHappiness = 100f;
    [SerializeField]private float decayRate = 10f;
    [SerializeField]private float currentHappiness;

    [SerializeField]private Slider happinessBar;

    private void Start()
    {
        currentHappiness = maxHappiness;
        happinessBar.value = currentHappiness;
    }

    private void Update()
    {
        currentHappiness -= decayRate * Time.deltaTime; 
        currentHappiness = Mathf.Clamp(currentHappiness, 0f, maxHappiness);

        if (happinessBar != null)
        {
            happinessBar.value = currentHappiness / maxHappiness;
        }

        if (currentHappiness <= 0f)
        {
            Debug.Log("Game Over");
        }
    }

    public void AddHappiness(float happinessValue)
    {
        currentHappiness += happinessValue;
        currentHappiness = Mathf.Clamp(currentHappiness, 0f, maxHappiness);

        if (happinessBar != null)
        {
            happinessBar.value = currentHappiness / maxHappiness;
        }
    }
}

