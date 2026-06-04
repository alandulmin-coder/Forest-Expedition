using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject settingsPanel;

    [Header("Player")]
    public MonoBehaviour playerMovement;
    public MonoBehaviour playerInteraction;
    public MonoBehaviour photoSystem;

    private bool isPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isPaused)
            {
                PauseGame();
            }
            else
            {
                ResumeGame();
            }
        }
    }

    // =========================
    // PAUSE
    // =========================

    public void PauseGame()
    {
        isPaused = true;

        pausePanel.SetActive(true);

        Time.timeScale = 0f;

        Cursor.lockState =
            CursorLockMode.None;

        Cursor.visible = true;

        playerMovement.enabled = false;
        playerInteraction.enabled = false;
        photoSystem.enabled = false;
    }

    // =========================
    // RESUME
    // =========================

    public void ResumeGame()
    {
        isPaused = false;

        pausePanel.SetActive(false);

        settingsPanel.SetActive(false);

        Time.timeScale = 1f;

        Cursor.lockState =
            CursorLockMode.Locked;

        Cursor.visible = false;

        playerMovement.enabled = true;
        playerInteraction.enabled = true;
        photoSystem.enabled = true;
    }

    // =========================
    // SETTINGS
    // =========================

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    // =========================
    // MENU
    // =========================

    public void BackToMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene("MainMenu");
    }
}