//
// © Arthur Vasseur arthurvasseur.fr
//

using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]private string firstLevel;
        [SerializeField]private GameObject panelFeedBack;
    
        private int firstTimeLaunch;
        private void Start()
        {
            firstTimeLaunch = PlayerPrefs.GetInt( "HasPlayed");
            if (firstTimeLaunch == 0 )
                PlayerPrefs.SetInt( "HasPlayed", 1 );
            else panelFeedBack.SetActive(true);
            PlayerPrefs.SetInt("score", 0);
        }
        public void StartGame()
        {
            PlayerPrefs.SetInt("currentLives", 3);
            PlayerPrefs.SetInt("score", 0);
            SceneManager.LoadScene(firstLevel);
        }
        public void QuitGame() => Application.Quit();
        public void GiveFeedBack() => Application.OpenURL("https://play.google.com/store/apps/details?id=com.ArthaiirGames.SpaceActionHero");
        public void OpenLink(string link) => Application.OpenURL(link);
    }
}
