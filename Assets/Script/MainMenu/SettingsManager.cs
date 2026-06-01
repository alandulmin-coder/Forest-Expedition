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

    public PlayerMovement playerMovement;

    void Start()
    {
        bgmSlider.onValueChanged.AddListener(SetBGMVolume);

        sensitivitySlider.onValueChanged.AddListener(SetSensitivity);
    }

    public void SetBGMVolume(float volume)
    {
        bgmSource.volume = volume;
    }

    public void SetSensitivity(float sensitivity)
    {
        playerMovement.lookSpeed = sensitivity;
    }
}