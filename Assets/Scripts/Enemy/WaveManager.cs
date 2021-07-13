//
// © Arthur Vasseur arthurvasseur.fr
//
using System;
using UnityEngine;

public class WaveManager : MonoBehaviour
{
    [Tooltip("Instance")] public static WaveManager instance;
    [SerializeField][Tooltip("Score value to add to score")]public WaveObject[] waves;
    [SerializeField][Tooltip("Time to next wave")]private float timeToNexWave;
    [SerializeField][Tooltip("WaveManager can spawn wave ?")]private bool canSpawnWaves;
    [SerializeField] private string nextLevel;
    public bool CanSpawnWaves { get => canSpawnWaves; set => canSpawnWaves = value; }

    public string NextLevel => nextLevel;
    private bool doOnce = true;
    private int currentWave;
    public void Awake()
    {
        instance = this;
        doOnce = true;
    }
    private void Start()
    {
        if (waves.Length !=0)
            timeToNexWave = waves[0].timeToSpawn;
    }
    private void Update()
    {
        if (canSpawnWaves) {
            timeToNexWave -= Time.deltaTime;
            if (timeToNexWave <= 0) {
                Instantiate(waves[currentWave].wave, waves[currentWave].position);
                if (currentWave < waves.Length - 1) {
                    currentWave++;
                    timeToNexWave = waves[currentWave].timeToSpawn;
                }
                else canSpawnWaves = false;
            }
        }
        else if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && GameObject.FindGameObjectsWithTag("Boss").Length == 0 && doOnce && !GameManager.instance.isDead) {
                doOnce = false;
                StartCoroutine(GameManager.instance.EndLevel()); 
        }
    }
}

[Serializable]
public struct WaveObject
{
    public float timeToSpawn;
    public GameObject wave;
    public Transform position;
}
