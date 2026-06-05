using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuManager : MonoBehaviour
{
    // =================================================
    // PANELS
    // =================================================

    [Header("Panels")]
    public GameObject mainPanel;
    public GameObject settingsPanel;
    public GameObject creditsPanel;

    // =================================================
    // AUDIO
    // =================================================

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("Audio Clips")]
    public AudioClip buttonClickSFX;

    // =================================================
    // SETTINGS
    // =================================================

    [Header("Settings")]
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Slider sensitivitySlider;

    // =================================================
    // START
    // =================================================

    void Start()
    {
        // Panel setup
        mainPanel.SetActive(true);

        settingsPanel.SetActive(false);

        creditsPanel.SetActive(false);

        // Cursor
        Cursor.lockState =
            CursorLockMode.None;

        Cursor.visible = true;

        // =========================
        // LOAD SETTINGS
        // =========================

        bgmSlider.value =
            PlayerPrefs.GetFloat("BGM", 1f);

        sfxSlider.value =
            PlayerPrefs.GetFloat("SFX", 1f);

        sensitivitySlider.value =
            PlayerPrefs.GetFloat(
                "Sensitivity",
                2f
            );

        // Apply loaded settings
        SetBGMVolume(bgmSlider.value);

        SetSFXVolume(sfxSlider.value);

        // Slider listeners
        bgmSlider.onValueChanged
            .AddListener(SetBGMVolume);

        sfxSlider.onValueChanged
            .AddListener(SetSFXVolume);

        sensitivitySlider.onValueChanged
            .AddListener(SetSensitivity);
    }

    // =================================================
    // AUDIO
    // =================================================

    public void PlayButtonSFX()
    {
        sfxSource.PlayOneShot(
            buttonClickSFX
        );
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;

        PlayerPrefs.SetFloat(
            "BGM",
            volume
        );
    }

    public void SetSFXVolume(float volume)
    {
        sfxSource.volume = volume;

        PlayerPrefs.SetFloat(
            "SFX",
            volume
        );
    }

    // =================================================
    // SENSITIVITY
    // =================================================

    public void SetSensitivity(float value)
    {
        PlayerPrefs.SetFloat(
            "Sensitivity",
            value
        );
    }

    // =================================================
    // START GAME
    // =================================================

    public void StartGame()
    {
        SceneManager.LoadScene(
            "Gameplay"
        );
    }

    // =================================================
    // SETTINGS
    // =================================================

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

    // =================================================
    // CREDITS
    // =================================================

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

    // =================================================
    // QUIT
    // =================================================

    public void QuitGame()
    {
        Application.Quit();

        Debug.Log("Quit Game");
    }
}