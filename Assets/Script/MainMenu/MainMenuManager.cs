using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject settingsPanel;
    public GameObject creditsPanel;

    // =========================
    // START
    // =========================

    public void StartGame()
    {
        SceneManager.LoadScene("Gameplay");
    }

    // =========================
    // SETTINGS
    // =========================

    public void OpenSettings()
    {
        mainPanel.SetActive(false);

        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);

        mainPanel.SetActive(true);
    }

    // =========================
    // CREDITS
    // =========================

    public void OpenCredits()
    {
        mainPanel.SetActive(false);

        creditsPanel.SetActive(true);
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);

        mainPanel.SetActive(true);
    }

    // =========================
    // QUIT
    // =========================

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Quit Game");
    }
}