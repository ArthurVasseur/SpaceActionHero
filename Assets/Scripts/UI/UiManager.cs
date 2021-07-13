//
// © Arthur Vasseur arthurvasseur.fr
//

using Player;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class UiManager : MonoBehaviour
    {
        public static UiManager instance;
        [Tooltip("GameOver panel")]public GameObject gameOverScreen;
        [SerializeField][Tooltip("Text current lives")]private Text livesText;
        [SerializeField][Tooltip("Text current score")]private Text scoreText;
        [SerializeField][Tooltip("Text hiScore")]private Text hiScoreText;
        [SerializeField][Tooltip("Panel level complete")]private GameObject levelComplete;
        [SerializeField] private Text endLevelScore, endCurrentScore, timeToNextLevel;
        [SerializeField][Tooltip("Panel pause")]private GameObject pauseScreen;
        [SerializeField][Tooltip("Slider boss health")]private Slider bossHealth;
        [SerializeField][Tooltip("Text boss name")]private Text bossName;
    
        private const string MainMenuName = "MainMenu";
        public string LivesText { set => livesText.text = "X" + value; }
        public string ScoreText { set => scoreText.text = "Score: " + value; }
        public string HiScoreText { set => hiScoreText.text = "Hi-Score : " + value; }
        public GameObject LevelComplete { get => levelComplete; set => levelComplete = value; }
        public Text EndLevelScore { get => endLevelScore; set => endLevelScore = value; }
        public Text EndCurrentScore { get => endCurrentScore; set => endCurrentScore = value; }
        public Text TimeToNextLevel { get => timeToNextLevel; set => timeToNextLevel = value; }
        public GameObject PauseScreen { get => pauseScreen; set => pauseScreen = value; }
        public Slider BossHealth { get => bossHealth; set => bossHealth = value; }
        public Text BossName { get => bossName; set => bossName = value; }
    
        private void Awake()
        {
            instance = this;
            if (GameManager.instance.gameObject.scene.buildIndex == -1) {
                LivesText = HealthManager.instance.CurrentHealth.ToString();
                ScoreText = GameManager.instance.LevelScore.ToString();
                HiScoreText = GameManager.instance.HighScore.ToString();
            }
        }
        public void Restart()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1f;
        }
        public void MainMenu()
        {
            SceneManager.LoadScene(MainMenuName);
            Time.timeScale = 1f;
        }
        public void Resume()
        {
            GameManager.PauseUnPause();
        }
        public void ActivateBossUi()
        {
            bossHealth.gameObject.SetActive(true);
            bossName.gameObject.SetActive(true);
            bossHealth.transform.parent.gameObject.SetActive(true);
        }
    }
}
