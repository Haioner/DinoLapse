using System.Collections;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;
using TMPro;
using UnityEngine.SceneManagement;

public class Options : MonoBehaviour
{
    [Header("CACHE")]
    [SerializeField] private CanvasGroup CG;
    [SerializeField] private KeyCode optionsKey = KeyCode.Escape;
    [SerializeField] private GameObject backToMenuButton;
    [SerializeField] private bool isOnMenu = false;

    [Header("Video")]
    [Header("Quality")]
    [SerializeField] private TMP_Dropdown qualityDropdown;
    private int currentQuality;

    [Header("Display")]
    [SerializeField] private FullScreenMode currentScreenMode;
    [SerializeField] private TextMeshProUGUI displayModeText;
    private int currentDisplayMode = 1;

    [Header("Resolution")]
    [SerializeField] private Vector2[] resolutions;
    [SerializeField] private TextMeshProUGUI resolutionText;
    private int currentResolution;

    [Header("RenderScale")]
    [SerializeField] private UniversalRenderPipelineAsset urpAsset;
    [SerializeField] private TextMeshProUGUI renderScaleText;
    [SerializeField] private Slider renderScaleSlider;
    private float currentRenderScale;

    [Header("PostProcessing")]
    [SerializeField] private List<Volume> allVolumes = new List<Volume>();
    [SerializeField] private Toggle postToggle;
    private int currentPostProcessing;

    [Header("Brightness")]
    [SerializeField] private Slider brightnessSlider;
    private int currentBrightness;

    [Header("Audio")]
    [SerializeField] private AudioMixer audioMixer;
    [SerializeField] private Slider masterSlider;
    [SerializeField] private Slider soundSlider;
    [SerializeField] private Slider musicSlider;
    [SerializeField] private TextMeshProUGUI masterText;
    [SerializeField] private TextMeshProUGUI soundText;
    [SerializeField] private TextMeshProUGUI musicText;
    private float currentMaster;
    private float currentSound;
    private float currentMusic;

    [Header("Geral")]
    [SerializeField] private Slider sensitivitySlider;
    [SerializeField] private TextMeshProUGUI sensitivityText;
    private float currentSensitivity;

    private void Awake()
    {
        LoadVideoSettings();
        LoadGeralSettings();
    }

    private void Start()
    {
        LoadAudioSettings();
    }

    private void Update()
    {
        ButtonChangeState();
    }

    public void ChangeCGState()
    {
        if (!CG.interactable)
        {
            CG.alpha = 1;
            PauseGame();

            if (!isOnMenu)
                CursorMode(true);
        }
        else
        {
            CG.alpha = 0;
            Time.timeScale = 1;

            if (!isOnMenu)
                CursorMode(false);
        }

        CG.interactable = !CG.interactable;
        CG.blocksRaycasts = CG.interactable;

    }

    private void CursorMode(bool state)
    {
        if (state)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }

    private void PauseGame()
    {
        Time.timeScale = 0;
        
    }

    private void ButtonChangeState()
    {
        if (Input.GetKeyDown(optionsKey))
            ChangeCGState();
    }

    public void BackToMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    #region Video

    public void ApplyVideoSettings()
    {
        ApplyQuality();
        ApplyDisplayMode();
        ApplyResolution();
        ApplyRenderScale();
        ApplyBrightness();
        ApplyPostProcessing();
    }

    private void LoadVideoSettings()
    {
        LoadQuality();
        LoadDisplay();
        LoadResolution();
        LoadRenderScale();
        LoadBrightness();
        LoadPostProcessing();
    }

    #region Quality

    public void SwichQuality()
    {
        currentQuality = qualityDropdown.value;
    }

    private void ApplyQuality()
    {
        QualitySettings.SetQualityLevel(currentQuality, true);
        SaveQuality();
    }

    private void SaveQuality()
    {
        PlayerPrefs.SetInt("data_quality", currentQuality);
    }

    private void LoadQuality()
    {
        if (PlayerPrefs.HasKey("data_quality"))
        {
            currentQuality = PlayerPrefs.GetInt("data_quality");
            qualityDropdown.SetValueWithoutNotify(currentQuality);
        }
        else
        {
            currentQuality = 5;
        }
        ApplyQuality();
    }
    
    #endregion

    #region DisplayMode

    public void NextDisplayMode()
    {
        if (currentDisplayMode < 3)
            currentDisplayMode++;
        DisplayText();
    }

    public void PreviousDisplayMode()
    {
        if (currentDisplayMode > 0)
            currentDisplayMode--;
        DisplayText();
    }

    private void ApplyDisplayMode()
    {
        DisplayText();
        Screen.fullScreenMode = currentScreenMode;
        SaveDisplay();
    }

    private void DisplayText()
    {
        switch (currentDisplayMode)
        {
            case 3:
                currentScreenMode = FullScreenMode.Windowed;
                break;
            case 2:
                currentScreenMode = FullScreenMode.MaximizedWindow;
                break;
            case 1:
                currentScreenMode = FullScreenMode.FullScreenWindow;
                break;
            case 0:
                currentScreenMode = FullScreenMode.ExclusiveFullScreen;
                break;
        }
        displayModeText.text = currentScreenMode.ToString();
    }

    private void SaveDisplay()
    {
        PlayerPrefs.SetInt("display", currentDisplayMode);
    }

    private void LoadDisplay()
    {
        if (PlayerPrefs.HasKey("display"))
        {
            currentDisplayMode = PlayerPrefs.GetInt("display");
            ApplyDisplayMode();
        }
    }
    #endregion

    #region Resolution
    public void NextResolution()
    {
        if (currentResolution < resolutions.Length - 1)
            currentResolution++;
        ResolutionText();
    }

    public void PreviousResolution()
    {
        if (currentResolution > 0)
            currentResolution--;
        ResolutionText();
    }

    private void ApplyResolution()
    {
        ResolutionText();
        Screen.SetResolution((int)resolutions[currentResolution].x, (int)resolutions[currentResolution].y, currentScreenMode);
        SaveResolution();
    }

    private void ResolutionText()
    {
        resolutionText.text = resolutions[currentResolution].x.ToString() + "x" + resolutions[currentResolution].y.ToString();
    }

    private void SaveResolution()
    {
        PlayerPrefs.SetInt("resolution", currentResolution);
    }

    private void LoadResolution()
    {
        if (PlayerPrefs.HasKey("resolution"))
        {
            currentResolution = PlayerPrefs.GetInt("resolution");
        }
            ApplyResolution();
    }
    #endregion

    #region RenderScale

    public void RenderScaleValue(float sliderValue)
    {
        currentRenderScale = sliderValue;
        renderScaleText.text = currentRenderScale.ToString("F1");
    }

    private void ApplyRenderScale()
    {
        urpAsset.renderScale = currentRenderScale;
        SaveRenderScale();
    }

    private void SaveRenderScale()
    {
        PlayerPrefs.SetFloat("renderScale", currentRenderScale);
    }

    private void LoadRenderScale()
    {
        if (PlayerPrefs.HasKey("renderScale"))
        {
            currentRenderScale = PlayerPrefs.GetFloat("renderScale");
            renderScaleSlider.SetValueWithoutNotify(currentRenderScale);
        }
        else
        {
            currentRenderScale = 0.5f;
        }
        renderScaleText.text = currentRenderScale.ToString("F1");
        ApplyRenderScale();
    }
    #endregion

    #region Brightnees

    public void BrightnessValue(float sliderValue)
    {
        currentBrightness = (int)sliderValue;
    }

    private void ApplyBrightness()
    {
        ColorAdjustments coloradj;
        for (int i = 0; i < allVolumes.Count; i++)
        {
            if (allVolumes[i].sharedProfile.TryGet<ColorAdjustments>(out coloradj))
            {
                coloradj.postExposure.value = currentBrightness;
            }
        }
        SaveBrightness();
    }

    private void SaveBrightness()
    {
        PlayerPrefs.SetInt("brightness", currentBrightness);
    }

    private void LoadBrightness()
    {
        if (PlayerPrefs.HasKey("brightness"))
        {
            currentBrightness = PlayerPrefs.GetInt("brightness");
            brightnessSlider.value = currentBrightness;
        }
        else
        {
            currentBrightness = 0;
        }
        BrightnessValue(currentBrightness);
    }
    #endregion

    #region PostProcessing

    public void SwichPostProcessing()
    {
        if (currentPostProcessing == 0)
            currentPostProcessing = 1;
        else
            currentPostProcessing = 0;
    }

    private void ApplyPostProcessing()
    {
        PostProcessingLoader[] allPostLoaders = FindObjectsOfType<PostProcessingLoader>();
        foreach (PostProcessingLoader item in allPostLoaders)
        {
            item.SetPostState(postToggle.isOn);
        }

        if (currentPostProcessing == 0)
            brightnessSlider.interactable = false;
        else
            brightnessSlider.interactable = true;

        SavePostProcessing();
    }

    private void SavePostProcessing()
    {
        PlayerPrefs.SetInt("postprocessing", currentPostProcessing);
    }

    private void LoadPostProcessing()
    {
        if (PlayerPrefs.HasKey("postprocessing"))
        {
            currentPostProcessing = PlayerPrefs.GetInt("postprocessing");
            if (currentPostProcessing == 0)
                postToggle.SetIsOnWithoutNotify(false);
            else
                postToggle.SetIsOnWithoutNotify(true);
        }
        else
        {
            currentPostProcessing = 1;
        }
        ApplyPostProcessing();
    }
    #endregion

    #endregion

    #region Audio

    private void LoadAudioSettings()
    {
        LoadMasterVolume();
        LoadSoundVolume();
        LoadMusicVolume();
    }

    private float AudioValueText(float value)
    {
        return value * 100;
    }

    #region Master
    public void MasterVolume()
    {
        currentMaster = masterSlider.value;
        audioMixer.SetFloat("MasterVolume", Mathf.Log10(masterSlider.value) * 20);
        PlayerPrefs.SetFloat("MVolume", masterSlider.value);
        MasterText();
    }

    private void MasterText()
    {
        masterText.text = AudioValueText(currentMaster).ToString("F0");
    }

    private void LoadMasterVolume()
    {
        if (PlayerPrefs.HasKey("MVolume"))
        {
            currentMaster = PlayerPrefs.GetFloat("MVolume");
            masterSlider.SetValueWithoutNotify(currentMaster);
        }
        MasterVolume();
    }
    #endregion

    #region Sound
    public void SoundVolume()
    {
        currentSound = soundSlider.value;
        audioMixer.SetFloat("SoundVolume", Mathf.Log10(soundSlider.value) * 20);
        PlayerPrefs.SetFloat("SVolume", soundSlider.value);
        SoundText();
    }

    private void SoundText()
    {
        soundText.text = AudioValueText(currentSound).ToString("F0");
    }

    private void LoadSoundVolume()
    {
        if (PlayerPrefs.HasKey("SVolume"))
        {
            currentSound = PlayerPrefs.GetFloat("SVolume");
            soundSlider.SetValueWithoutNotify(currentSound);
        }
        SoundVolume();
    }
    #endregion

    #region Music
    public void MusicVolume()
    {
        currentMusic = musicSlider.value;
        audioMixer.SetFloat("MusicVolume", Mathf.Log10(musicSlider.value) * 20);
        PlayerPrefs.SetFloat("MuVolume", musicSlider.value);
        MusicText();
    }

    private void MusicText()
    {
        musicText.text = AudioValueText(currentMusic).ToString("F0");
    }

    private void LoadMusicVolume()
    {
        if (PlayerPrefs.HasKey("MuVolume"))
        {
            currentMusic = PlayerPrefs.GetFloat("MuVolume");
            musicSlider.SetValueWithoutNotify(currentMusic);
        }
        MusicVolume();
    }
    #endregion

    #endregion

    #region Geral

    private void LoadGeralSettings()
    {
        LoadSensitivity();
    }

    public void MouseSensitivity()
    {
        currentSensitivity = sensitivitySlider.value;
        PlayerPrefs.SetFloat("Sensitivity", currentSensitivity);
        SetSensitivity();
        SensitivityText();
    }

    private void SetSensitivity()
    {
        if(FindObjectOfType<MouseSensitivityManager>() != null)
        {
            FindObjectOfType<MouseSensitivityManager>().SetSensitivity(currentSensitivity);
        }
    }

    private void SensitivityText()
    {
        sensitivityText.text = currentSensitivity.ToString("F0");
    }

    private void LoadSensitivity()
    {
        if (PlayerPrefs.HasKey("Sensitivity"))
        {
            currentSensitivity = PlayerPrefs.GetFloat("Sensitivity");
            sensitivitySlider.SetValueWithoutNotify(currentSensitivity);
        }
        MouseSensitivity();
    }

    #endregion
}
