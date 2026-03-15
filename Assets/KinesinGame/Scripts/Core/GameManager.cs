using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public PlayerController playerController;
    public HappinessSystem happinessSystem;
    public DistanceTracker distanceTracker;
    public UIController uiController;
    public GroundSpawner groundSpawner;

    public bool IsGameOver { get; private set; }
    public float DistanceScore { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
    }

    private void Start()
    {
        IsGameOver = false;
        DistanceScore = 0f;

        uiController.HidePanel(UIPanelType.GameOver);
        uiController.ShowPanel(UIPanelType.Happiness);
        uiController.ShowPanel(UIPanelType.Distance);

        if (groundSpawner != null)
        {
            groundSpawner.SpawnTile();
        }

        DebugColor.Log("Game Scene Started", "green");
    }

    public void SetDistance(float distance)
    {
        if (IsGameOver) return;

        DistanceScore = distance;
        uiController.UpdateDistance(DistanceScore);
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void ReturnToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }

    public void GameOver(string reason)
    {
        if (IsGameOver) return;

        IsGameOver = true;

        StartCoroutine(GameOverSequence());

        DebugColor.Log($"Game Over: {reason}, Distance: {Mathf.FloorToInt(DistanceScore)}", "red");
    }
    private IEnumerator GameOverSequence()
    {
        if (playerController != null)
        {
            playerController.Die();
        }

        // wait for death animation
        yield return new WaitForSeconds(1.1f);

        if (uiController != null)
        {
            uiController.ShowPanel(UIPanelType.GameOver);
        }
    }
}