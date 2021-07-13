//
// © Arthur Vasseur arthurvasseur.fr
//
using System.Collections;
using Ads;
using Player;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;
    [SerializeField][Tooltip("Game score")]private int score = 0;
    [SerializeField]private float waitForLevelEnd = 5f;
    
    private bool levelEnding;
    private int levelScore;
    private bool canPause;
    public bool isDead;
    public int HighScore { get; private set; }
    public int LevelScore => score;

    private void Awake()
    {
        instance = this;
        isDead = false;
    }

    private void Start()
    {
        PlayerController.instance.DisableInputs = false;
        HighScore = PlayerPrefs.HasKey("highScore") ? PlayerPrefs.GetInt("highScore") : 0;
        score = PlayerPrefs.HasKey("score") ? PlayerPrefs.GetInt("score") : 0;
        UiManager.instance.HiScoreText = HighScore.ToString();
        UiManager.instance.ScoreText = score.ToString();
        canPause = true;
        
    }
    private void Update()
    {
        if (levelEnding)
            PlayerController.instance.transform.position += new Vector3(0.007f * Time.deltaTime, 0f, 0f);
        if (Input.GetKeyDown(KeyCode.Escape) && canPause)
            PauseUnPause();
    }
    public void KillPlayer()
    {
        UiManager.instance.gameOverScreen.SetActive(true);
        WaveManager.instance.CanSpawnWaves = false;
        MusicController.instance.PlayGameOver();
        PlayerPrefs.SetInt("highScore", score);
        canPause = false;
        isDead = true;
        if (AutomaticAd.instance != null && !AdsController.DisableAds)
            AutomaticAd.instance.ShowRewardedVideo();
        score = 0;
        PlayerPrefs.SetInt("score", 0);
    }
    public void AddScore(int value)
    {
        if (!isDead || !levelEnding ) {
            score += value;
            levelScore += value;
            UiManager.instance.ScoreText = score.ToString();
            if (score > HighScore) {
                HighScore = score;
                UiManager.instance.HiScoreText = HighScore.ToString();
            }
        }
    }
    public IEnumerator EndLevel()
    {
        UiManager.instance.EndLevelScore.text = "LevelScore : " + levelScore;
        UiManager.instance.EndCurrentScore.text = "Total Score : " + score;
        UiManager.instance.LevelComplete.SetActive(true);
        PlayerController.instance.DisableInputs = true;
        levelEnding = true;
        MusicController.instance.PlayVictory();
        canPause = false;
        yield return new WaitForSeconds(0.5f);
        UiManager.instance.EndLevelScore.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.5f);
        PlayerPrefs.SetInt("score", score);
        UiManager.instance.EndCurrentScore.gameObject.SetActive(true);
        PlayerPrefs.SetInt("highScore", score);
        if (AutomaticAd.instance && !AdsController.DisableAds)
            AutomaticAd.instance.ShowRewardedVideo();
        float totalTime = waitForLevelEnd;
        while(totalTime >= 0) {
            totalTime -= Time.deltaTime;
            int time = (int)totalTime; 
            UiManager.instance.TimeToNextLevel.text = time.ToString();
            yield return null;
        }
        if (WaveManager.instance.NextLevel != "FinalScene") {
            DontDestroyOnLoad(gameObject);
            DontDestroyOnLoad(PlayerController.instance);
        }
        SceneManager.LoadScene(WaveManager.instance.NextLevel);
        PlayerController.instance.DisableInputs = false;
    }
    public static void PauseUnPause()
    {
        if (UiManager.instance.PauseScreen.activeInHierarchy) {
            UiManager.instance.PauseScreen.SetActive(false);
            Time.timeScale = 1f;
            PlayerController.instance.DisableInputs = false;
        } else {
            UiManager.instance.PauseScreen.SetActive(true);
            Time.timeScale = 0f;
            PlayerController.instance.DisableInputs = true;
        }
    }
    public static void SetGamePause(bool value)
    {
        if (value) {
            PlayerController.instance.DisableInputs = true;
            Time.timeScale = 0f;
        } else {
            PlayerController.instance.DisableInputs = false;
            Time.timeScale = 1f;
        }
    }
}
