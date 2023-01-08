using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public Image fadeImg;
    public Text wave_Txt;
    public Text score_Txt;
    public GameObject pauseMenu;
    public GameObject gameOverMenu;
    public List<GameObject> e_SpawnerList;

    public static GameManager gmg;
    public static int score;
    public static int wave = 1;
    public static int maxWave;
    public int enemyCount = 0;

    // Start is called before the first frame update
    void Awake()
    {
        gmg = GetComponent<GameManager>();
        score = 0;
        
    }

    private void Start()
    {
        StartCoroutine(FadeIn(2f, true));
    }

    // Update is called once per frame
    void Update()
    {
        score_Txt.text = score.ToString();
        wave_Txt.text = wave.ToString();

        if (Input.GetKey(KeyCode.Escape))
        {
            OpenPasueMenu();
        }
    }

    public void ScoreUp(int e_score)
    {
        enemyCount++;

        score = score + e_score;

        WaveStrong();
    }

    public void WaveStrong()
    {
        if (enemyCount % 30 == 0)
        {
            wave += 1;
        }

        if (wave == 5)
        {
            Debug.Log("°­ÇØÁü");
            e_SpawnerList[1].SetActive(true);
        }
    }

    public void OpenPasueMenu()
    {
        Time.timeScale = 0;
        pauseMenu.SetActive(true);
    }

    public void PushResumeButton()
    {
        Time.timeScale = 1;
        pauseMenu.SetActive(false);
    }

    public void PushExitButton()
    {
        SceneManager.LoadScene(0);
    }

    public void RetryButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void PushNextButton()
    {
        wave = 1;
        score = 0;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void OpenGameOver()
    {
        gameOverMenu.SetActive(true);
    }

    IEnumerator FadeIn(float fadeTime, bool isFadeEnded)
    {
        float t = 0;

        while (t < fadeTime)
        {
            t += Time.deltaTime;

            float percent = t / fadeTime;

            if (isFadeEnded)

                fadeImg.color = new Color(fadeImg.color.r, fadeImg.color.g, fadeImg.color.b, Mathf.Lerp(1f, 0, percent));


            yield return null;

        }

        isFadeEnded = false;
    }
}
