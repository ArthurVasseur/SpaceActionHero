//
// © Arthur Vasseur arthurvasseur.fr
//
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameComplete : MonoBehaviour
{
    [SerializeField][Tooltip("Time before each text appear")]private float timeBetweenTexts;
    [SerializeField]private string mainMenu;
    [SerializeField]private Text message, score, pressKey;
    private bool canExit;
    private void Start()
    {
        StartCoroutine(ShowText());
    }
    private void Update()
    {
        if (canExit && Input.anyKeyDown)
            SceneManager.LoadScene(mainMenu);
    }
    private IEnumerator ShowText()
    {
        yield return new WaitForSeconds(timeBetweenTexts);
        message.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeBetweenTexts);
        score.text = "Final score : " + PlayerPrefs.GetInt("score");
        score.gameObject.SetActive(true);
        yield return new WaitForSeconds(timeBetweenTexts);
        pressKey.gameObject.SetActive(true);
        canExit = true;
    }
}
