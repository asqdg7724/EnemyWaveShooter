using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainManager : MonoBehaviour
{
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

    }

    // Update is called once per frame
    void Update()
    {
        
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
