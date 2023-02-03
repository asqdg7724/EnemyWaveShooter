using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
    [SerializeField]
    private Dropdown resolutionDropdown;
    private Resolution[] resolutions;
    private List<Resolution> filteredResolutions;

    private float currentRefreshRate;
    private int currentResolutionIndex = 0;

    public GameObject optionMenu;
    public AudioSource music;
    public Button starBtn;
    public Button optionBtn;
    public Button exitBtn;

    public static float musicVolume { get; private set; }
    public static float soundEffectsVolume { get; private set; }

    // Start is called before the first frame update
    void Start()
    {
        resolutions = Screen.resolutions;
        filteredResolutions = new List<Resolution>();

        resolutionDropdown.ClearOptions();
        currentRefreshRate = Screen.currentResolution.refreshRate;

        for (int i = 0; i < resolutions.Length; i++)
        {
            if (resolutions[i].refreshRate == currentRefreshRate)
            {
                filteredResolutions.Add(resolutions[i]);
            }
        }

        List<string> options = new List<string>();
        for (int i = 0; i < filteredResolutions.Count; i++)
        {
            string resolutionOption = filteredResolutions[i].width + "x" + filteredResolutions[i].height + " " + filteredResolutions[i].refreshRate + " Hz";
            options.Add(resolutionOption);
            if (filteredResolutions[i].width == Screen.width && filteredResolutions[i].height == Screen.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();
    }

    public void SetResolution(int resolutionIndex)
    {
        Resolution resolution = filteredResolutions[resolutionIndex];
        Screen.SetResolution(resolution.width, resolution.height, true);
    }

    public void FullScreenChange()
    {
        Screen.fullScreen = !Screen.fullScreen;
    }

    public void PushStart()
    {
        SceneManager.LoadScene(1);
    }

    public void PushOption()
    {
        optionMenu.SetActive(true);
        
        starBtn.interactable = false;
        optionBtn.interactable = false;
        exitBtn.interactable = false;
    }

    public void PushOptionExit()
    {
        optionMenu.SetActive(false);

        starBtn.interactable = true;
        optionBtn.interactable = true;
        exitBtn.interactable = true;
    }

    public void PushExit()
    {
        // Application.Quit�� ���� �������α׷��̳� ����Ͽ����� �۵������� ����Ƽ �����Ϳ����� ���� X
        // �׷��� #if�� Ȱ���Ͽ� UnityEditor.EditorApplication.isPlaying�� �����Ϳ����� ����� �� �ְ� ó��
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;

        #else
        Application.Quit();

        #endif
    }

    public void OnMusicSliderValue(float value)
    {
        musicVolume = value;
    }

    public void OnSoundEffectSliderValue(float value)
    {
        soundEffectsVolume = value;
    }
}
