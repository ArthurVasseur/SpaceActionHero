//
// © Arthur Vasseur arthurvasseur.fr
//
using UnityEngine;
public class MusicController : MonoBehaviour
{
    public static MusicController instance;
    [SerializeField] private AudioSource levelMusic, bossMusic, victoryMusic, gameOverMusic;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        levelMusic.Play();
    }
    private void StopMusic()
    {
        levelMusic.Stop();
        bossMusic.Stop();
        victoryMusic.Stop();
        gameOverMusic.Stop();
    }
    public void PlayBoss()
    {
        StopMusic();
        bossMusic.Play();
    }
    public void PlayVictory()
    {
        StopMusic();
        victoryMusic.Play();
    }
    public void PlayGameOver()
    {
        StopMusic();
        gameOverMusic.Play();
    }
    public void PlayLevelMusic()
    {
        StopMusic();
        levelMusic.Play();
    }

    public void StopVictory()
    {
        victoryMusic.Stop();
    }
}
