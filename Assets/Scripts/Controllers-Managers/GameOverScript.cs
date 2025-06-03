using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameOverScript : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject gameOverPanel;

    [Header("Player Reference")]
    [SerializeField] private PlayerHealth playerHealth;


    void Start()
    {
        if(playerHealth != null)
            playerHealth = FindAnyObjectByType<PlayerHealth>();

        // Subscribe to player's death event
        playerHealth.OnPlayerDeath.AddListener(ShowGameOverUI);
    }

    void ShowGameOverUI()
    {
        gameOverPanel.SetActive(true);

        // Pause the game if needed
        //Time.timeScale = 0f;
    }

    public void RestartGame()
    {
        // Resume time before reloading
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        InventorySaveSystem.LoadInventory();
    }

    void OnDestroy()
    {
        // Unsubscribe to prevent memory leaks
        playerHealth.OnPlayerDeath.RemoveListener(ShowGameOverUI);
    }
}