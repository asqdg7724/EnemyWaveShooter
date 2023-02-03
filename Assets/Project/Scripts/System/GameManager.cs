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
    public GameObject gameClearMenu;
    public Text warningTxt;
    public GameObject boss;
    public List<GameObject> e_SpawnerList;
    SoundPlayer sound;

    public static GameManager gmg;
    public bool bossAppear;
    public static int score;
    public static int wave = 1;
    public static int maxWave;
    public int enemyCount = 0;

    // Start is called before the first frame update
    void Awake()
    {
        gmg = GetComponent<GameManager>();
        score = 0;
        sound = GetComponent<SoundPlayer>();
    }

    private void Start()
    {
        StartCoroutine(FadeIn(2f, true));
        bossAppear = false;
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
        if (enemyCount % 20 == 0 && enemyCount <= 280)
        {
            wave += 1;
        }

        if (wave == 3)
        {
            Debug.Log("°­ÇØÁü");
            e_SpawnerList[1].SetActive(true);
        }

        if (wave == 6)
        {
            Debug.Log("°­ÇØÁü");
            e_SpawnerList[2].SetActive(true);
        }

        if (wave == 9)
        {
            Debug.Log("°­ÇØÁü");
            e_SpawnerList[3].SetActive(true);
        }

        if (wave == 12)
        {
            Debug.Log("°­ÇØÁü");
            e_SpawnerList[4].SetActive(true);
        }

        if (wave == 15)
        {
            bossAppear = true;

            Debug.Log("°­ÇØÁü");
            //GameObject.Find("Boss").SetActive(true);
            //StartCoroutine(BlinkText(4f));
            if (bossAppear && enemyCount == 280)
            {
                StartCoroutine(BlinkText(4f));
            }
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
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void PushMain()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(0);
    }

    public void OpenGameOver()
    {
        gameOverMenu.SetActive(true);
    }

    public void OpenGameClear()
    {
        gameClearMenu.SetActive(true);
        sound.SoundPlay(0);
        Time.timeScale = 0;
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

    IEnumerator BlinkText(float delay)
    {
        float t = 0;

        while (t <= delay)
        {
            //t += Time.deltaTime;
            t++;

            warningTxt.color = new Color(warningTxt.color.r, warningTxt.color.g, warningTxt.color.b, 255);
            yield return new WaitForSeconds(0.5f);
            warningTxt.color = new Color(warningTxt.color.r, warningTxt.color.g, warningTxt.color.b, 0);
            yield return new WaitForSeconds(0.5f);
            warningTxt.color = new Color(warningTxt.color.r, warningTxt.color.g, warningTxt.color.b, 255);

            if (t == delay)
            {
                warningTxt.color = new Color(warningTxt.color.r, warningTxt.color.g, warningTxt.color.b, 0);
                break;
            }
        }
        boss.SetActive(true);
    }
}
