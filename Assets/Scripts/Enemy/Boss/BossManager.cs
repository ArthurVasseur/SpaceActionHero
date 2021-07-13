//
// © Arthur Vasseur arthurvasseur.fr
//
using System;
using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.Serialization;

public abstract class BossManager : MonoBehaviour
{
    [Tooltip("Instance")]public static BossManager instance;
    [SerializeField][Tooltip("Current heath of boss")]protected int currentHealth = 100;
    [SerializeField][Tooltip("Name of boss")]protected string bossName;
    [SerializeField][Tooltip("Can be null")] protected GameObject endExplosion;
    [SerializeField][Tooltip("Can be null")] protected float timeToExplosionEnd;
    [SerializeField]protected BattlePhase[] phases;
    protected int currentPhase;
    protected bool battleEnding;

    private void Awake()
    {
        instance = this;
    }

    protected virtual void Start()
    {
        UiManager.instance.BossName.text = bossName;
        UiManager.instance.BossHealth.maxValue = currentHealth;
        UiManager.instance.BossHealth.value = currentHealth;
        UiManager.instance.ActivateBossUi();
        if (MusicController.instance)
            MusicController.instance.PlayBoss();
    }

    protected virtual void Update()
    {
    }

    public void HurtBoss(int healthValue = 1)
    {
        currentHealth -= healthValue;
        UiManager.instance.BossHealth.value = currentHealth;
        if (currentHealth <= 0 && !battleEnding) {
            battleEnding = true;
            StartCoroutine(EndBattle());
        }
    }

    protected virtual IEnumerator EndBattle()
    {
        if (endExplosion)
            Instantiate(endExplosion, phases[phases.Length - 1].removeAtPhaseEnd.transform.position, gameObject.transform.rotation);

        if (phases[phases.Length - 1].removeAtPhaseEnd) {
            phases[phases.Length - 1].removeAtPhaseEnd.GetComponent<SpriteRenderer>().enabled = false;
            phases[phases.Length - 1].removeAtPhaseEnd.GetComponent<CircleCollider2D>().enabled = false;
            yield return new WaitForSeconds(timeToExplosionEnd);
        }
        else yield return null;
        StartCoroutine(GameManager.instance.EndLevel());
        Destroy(gameObject.transform.parent.gameObject);
    }
}

[Serializable]
public class BattleShot
{
    public GameObject shot;
    public float timeBetweenShots;
    public float shotCounter;
    public Transform firePoint;
}

[Serializable]
public class BattlePhase
{
    public BattleShot[] phaseShot;
    public int healthToEndPhase;
    public int score;
    public GameObject removeAtPhaseEnd;
    public GameObject addAtPhaseEnd;
    public Transform newSpawnPoint;
}