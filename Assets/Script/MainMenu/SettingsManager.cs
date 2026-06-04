using UnityEngine;
using UnityEngine.UI;

public class SettingsManager : MonoBehaviour
{
    [Header("Sliders")]
    public Slider bgmSlider;
    public Slider sfxSlider;
    public Slider sensitivitySlider;

    [Header("Audio")]
    public AudioSource bgmSource;

    [Header("Player")]
    public PlayerMovement playerMovement;

    void Start()
    {
        // Load saved settings
        bgmSlider.value =
            PlayerPrefs.GetFloat("BGM", 1f);

        sfxSlider.value =
            PlayerPrefs.GetFloat("SFX", 1f);

        sensitivitySlider.value =
            PlayerPrefs.GetFloat("Sensitivity", 2f);

        // Apply settings
        SetBGMVolume(bgmSlider.value);
        SetSensitivity(sensitivitySlider.value);

        // Listener
        bgmSlider.onValueChanged
            .AddListener(SetBGMVolume);

        sensitivitySlider.onValueChanged
            .AddListener(SetSensitivity);
    }

    // =========================
    // BGM
    // =========================

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;

        PlayerPrefs.SetFloat(
            "BGM",
            volume
        );
    }

    // =========================
    // SENSITIVITY
    // =========================

    public void SetSensitivity(float value)
    {
        if (playerMovement != null)
        {
            playerMovement.lookSpeed = value;
        }

        PlayerPrefs.SetFloat(
            "Sensitivity",
            value
        );
    }
}