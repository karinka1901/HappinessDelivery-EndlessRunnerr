using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.UI;

public class HappinessSystem : MonoBehaviour
{
    [SerializeField]private float maxHappiness = 100f;
    [SerializeField]private float decayRate = 4f;
    [SerializeField]private float currentHappiness;

    [SerializeField]private Slider happinessBar;

    [SerializeField] private Sprite[] happinessSprites;
    [SerializeField] private Image happinessState;

    private void Start()
    {
        currentHappiness = maxHappiness;
        happinessBar.value = currentHappiness / maxHappiness;

        UpdateHappinessVisuals();
    }

    private void Update()
    {
        if (GameManager.Instance != null && GameManager.Instance.IsGameOver)
            return;

        currentHappiness -= decayRate * Time.deltaTime;
        currentHappiness = Mathf.Clamp(currentHappiness, 0f, maxHappiness);

        UpdateHappinessVisuals();

    }

    public void UpdateHappinessVisuals()
    {
        float happinessLevel = currentHappiness / maxHappiness;

        if (happinessBar != null)
        {
            happinessBar.value = happinessLevel;
        }

        if (happinessLevel > 0.66f)
        {
            happinessState.sprite = happinessSprites[0];
        }
        else if (happinessLevel > 0.33f)
        {
            happinessState.sprite = happinessSprites[1];
        }
        else if (happinessLevel > 0f)
        {
            happinessState.sprite = happinessSprites[2];
        }
        else
        {
            GameManager.Instance.GameOver("Happiness reached zero");
        }
    }

    public void AddHappiness(float happinessValue)
    {
        currentHappiness += happinessValue;
        currentHappiness = Mathf.Clamp(currentHappiness, 0f, maxHappiness);
        UpdateHappinessVisuals();
    }

    public float RemoveHappiness(float amount)
    {
        currentHappiness -= amount;
        currentHappiness = Mathf.Clamp(currentHappiness, 0f, maxHappiness);
        UpdateHappinessVisuals();
        return currentHappiness;
    }
}

