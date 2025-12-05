using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MenuManager : MonoBehaviour
{
    [Header("Panels")]
    public GameObject settingsPanel;
    public GameObject creditsPanel;   // add this for credits

    [Header("Settings UI")]
    public Slider volumeSlider;
    public Toggle fullscreenToggle;
    public TMP_Dropdown resolutionDropdown;

    [Header("Audio")]
    public AudioSource musicSource; // drag your AudioManager (with AudioSource) here

    private Resolution[] resolutions;

    // --- Play Button ---
    public void PlayGame()
    {
        SceneManager.LoadScene("Bedroom"); // replace with your starting scene
    }

    // --- Settings Button ---
    public void OpenSettings()
    {
        settingsPanel.SetActive(true);
    }

    public void CloseSettings()
    {
        settingsPanel.SetActive(false);
    }

    // --- Credits Button ---
    public void OpenCredits()
    {
        creditsPanel.SetActive(true);   // show credits panel
    }

    public void CloseCredits()
    {
        creditsPanel.SetActive(false);  // hide credits panel
    }

    void Start()
    {
        // --- Hide panels at start ---
        if (settingsPanel != null) settingsPanel.SetActive(false);
        if (creditsPanel != null) creditsPanel.SetActive(false);

        // --- Volume setup ---
        float savedVol = PlayerPrefs.GetFloat("Volume", 0.8f);
        volumeSlider.value = savedVol;
        musicSource.volume = savedVol;
        volumeSlider.onValueChanged.AddListener(SetVolume);

        if (!musicSource.isPlaying)
            musicSource.Play();

        // --- Fullscreen setup ---
        bool full = PlayerPrefs.GetInt("Fullscreen", 1) == 1;
        fullscreenToggle.isOn = full;
        Screen.fullScreen = full;
        fullscreenToggle.onValueChanged.AddListener(SetFullscreen);

        // --- Resolution setup ---
        resolutions = Screen.resolutions;
        resolutionDropdown.ClearOptions();
        var options = new System.Collections.Generic.List<string>();
        int currentResIndex = 0;

        for (int i = 0; i < resolutions.Length; i++)
        {
            string label = $"{resolutions[i].width} x {resolutions[i].height}";
            options.Add(label);

            if (resolutions[i].width == Screen.currentResolution.width &&
                resolutions[i].height == Screen.currentResolution.height)
            {
                currentResIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        int savedIndex = PlayerPrefs.GetInt("ResolutionIndex", currentResIndex);
        resolutionDropdown.value = savedIndex;
        resolutionDropdown.RefreshShownValue();
        resolutionDropdown.onValueChanged.AddListener(SetResolution);
    }

    // --- Settings Functions ---
    void SetVolume(float v)
    {
        musicSource.volume = v;
        PlayerPrefs.SetFloat("Volume", v);
    }

    void SetFullscreen(bool on)
    {
        Screen.fullScreen = on;
        PlayerPrefs.SetInt("Fullscreen", on ? 1 : 0);
    }

    void SetResolution(int index)
    {
        Resolution r = resolutions[index];
        Screen.SetResolution(r.width, r.height, Screen.fullScreen);
        PlayerPrefs.SetInt("ResolutionIndex", index);
    }
}
