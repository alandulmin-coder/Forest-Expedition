using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    // =================================================
    // PANELS
    // =================================================

    [Header("Panels")]
    public GameObject pausePanel;
    public GameObject settingsPanel;
    public GameObject winPanel;

    // =================================================
    // PLAYER
    // =================================================

    [Header("Player")]
    public MonoBehaviour playerMovement;
    public MonoBehaviour playerInteraction;
    public MonoBehaviour photoSystem;

    // =================================================
    // OBJECTIVES
    // =================================================

    [Header("Objectives")]
    public bool tabletCollected;
    public bool fossilCollected;
    public bool meteorCollected;

    // =================================================
    // OBJECTIVE UI
    // =================================================

    [Header("Tablet UI")]
    public TextMeshProUGUI tabletText;
    public Image tabletCheck;

    [Header("Fossil UI")]
    public TextMeshProUGUI fossilText;
    public Image fossilCheck;

    [Header("Meteor UI")]
    public TextMeshProUGUI meteorText;
    public Image meteorCheck;

    public Color completedColor = Color.gray;

    // =================================================
    // SETTINGS
    // =================================================

    [Header("Settings")]
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Slider sensitivitySlider;

    // =================================================
    // AUDIO
    // =================================================

    [Header("Audio Sources")]
    public AudioSource bgmSource;
    public AudioSource sfxSource;

    [Header("SFX Clips")]
    public AudioClip buttonClickSFX;
    public AudioClip photoSFX;
    public AudioClip pickupSFX;
    public AudioClip objectiveCompleteSFX;

    // =================================================
    // STATE
    // =================================================

    private bool isPaused = false;

    // =================================================
    // START
    // =================================================

    void Start()
    {
        // Panels
        pausePanel.SetActive(false);
        settingsPanel.SetActive(false);
        winPanel.SetActive(false);

        // Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

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
        SetSensitivity(
            sensitivitySlider.value
        );

        // Listeners
        bgmSlider.onValueChanged
            .AddListener(SetBGMVolume);

        sfxSlider.onValueChanged
            .AddListener(SetSFXVolume);

        sensitivitySlider.onValueChanged
            .AddListener(SetSensitivity);

        // Hide checkmarks
        tabletCheck.gameObject.SetActive(false);
        fossilCheck.gameObject.SetActive(false);
        meteorCheck.gameObject.SetActive(false);
    }

    // =================================================
    // UPDATE
    // =================================================

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

    // =================================================
    // PAUSE
    // =================================================

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

    // =================================================
    // SETTINGS
    // =================================================

    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    // =================================================
    // AUDIO
    // =================================================

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

    public void PlaySFX(AudioClip clip)
    {
        sfxSource.PlayOneShot(clip);
    }

    public void PlayButtonSFX()
    {
        PlaySFX(buttonClickSFX);
    }

    // =================================================
    // SENSITIVITY
    // =================================================

    public void SetSensitivity(float value)
    {
        PlayerMovement movement =
            playerMovement as PlayerMovement;

        if (movement != null)
        {
            movement.lookSpeed = value;
        }

        PlayerPrefs.SetFloat(
            "Sensitivity",
            value
        );
    }

    // =================================================
    // OBJECTIVES
    // =================================================

    public void CompleteObjective(string id)
    {
        switch (id)
        {
            case "Tablet":

                if (!tabletCollected)
                {
                    tabletCollected = true;

                    CompleteObjectiveUI(
                        tabletText,
                        tabletCheck
                    );
                }

                break;

            case "Fossil":

                if (!fossilCollected)
                {
                    fossilCollected = true;

                    CompleteObjectiveUI(
                        fossilText,
                        fossilCheck
                    );
                }

                break;

            case "Meteor":

                if (!meteorCollected)
                {
                    meteorCollected = true;

                    CompleteObjectiveUI(
                        meteorText,
                        meteorCheck
                    );
                }

                break;
        }

        CheckWinCondition();
    }

    void CompleteObjectiveUI(
        TextMeshProUGUI text,
        Image check
    )
    {
        text.color = completedColor;

        check.gameObject.SetActive(true);
    }

    // =================================================
    // WIN
    // =================================================

    void CheckWinCondition()
    {
        if (
            tabletCollected &&
            fossilCollected &&
            meteorCollected
        )
        {
            WinGame();
        }
    }

    void WinGame()
    {
        winPanel.SetActive(true);

        PlaySFX(objectiveCompleteSFX);
        bgmSource.Stop();

        Cursor.lockState =
            CursorLockMode.None;

        Cursor.visible = true;

        Time.timeScale = 0f;

        playerMovement.enabled = false;
        playerInteraction.enabled = false;
        photoSystem.enabled = false;
    }

    // =================================================
    // MENU
    // =================================================

    public void BackToMenu()
    {
        Time.timeScale = 1f;

        SceneManager.LoadScene(
            "MainMenu"
        );
    }
}