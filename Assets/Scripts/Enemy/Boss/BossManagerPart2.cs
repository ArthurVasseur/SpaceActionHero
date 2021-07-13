//
// © Arthur Vasseur arthurvasseur.fr
//
using System.Collections.Generic;
using UnityEngine;

public class BossManagerPart2 : BossManager
{
    [SerializeField][Tooltip("Shield Sprite")]private GameObject shield;
    [SerializeField][Tooltip("BossMinion rotating around boss")]private List<GameObject> minion;
    
    private float time = 0.3f;

    private bool doOnce = true;
    private void Awake()
    {
        instance = this;
    }

    protected override void Update()
    {
        base.Update();
        FlipSprite();
        shield.transform.Rotate(0, 0, 50 * Time.deltaTime);
        if (!battleEnding && currentHealth <= phases[currentPhase].healthToEndPhase) {
            if (phases[currentPhase].removeAtPhaseEnd)
                phases[currentPhase].removeAtPhaseEnd.SetActive(false);
            if (phases[currentPhase].addAtPhaseEnd && phases[currentPhase].newSpawnPoint && currentPhase == 0) {
                if (doOnce) {
                    StartCoroutine(BossMinion.DestroyMinion(minion, phases[currentPhase].addAtPhaseEnd));
                    doOnce = false;
                }
            }

            if (GameManager.instance) 
                GameManager.instance.AddScore(phases[currentPhase].score);
            currentPhase++;
        }
        else if (phases[currentPhase].phaseShot != null)
        {
            foreach (BattleShot shot in phases[currentPhase].phaseShot) {
                shot.shotCounter -= Time.deltaTime;
                if (shot.shotCounter <= 0) {
                    shot.shotCounter = shot.timeBetweenShots;
                    Instantiate(shot.shot, shot.firePoint.transform.position,
                        Quaternion.Euler(shot.firePoint.transform.position));
                }
            }
        }
    }

    private void FlipSprite()
    {
        time -= Time.deltaTime;
        if (time <= 0) {
            GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
            time = 0.3f;
        }
    }
}
