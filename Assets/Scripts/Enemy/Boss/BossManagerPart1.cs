//
// © Arthur Vasseur arthurvasseur.fr
//

using System.Collections;
using UnityEngine;

public class BossManagerPart1 : BossManager
{
    [SerializeField] [Tooltip("Animation")] private Animator bossAnim;
    private static readonly int Phase = Animator.StringToHash("Phase");

    private void Awake()
    {
        instance = this;
    }

    protected override void Update()
    {
        base.Update();
        if (!battleEnding && currentHealth <= phases[currentPhase].healthToEndPhase) {
            if (phases[currentPhase].removeAtPhaseEnd)
                phases[currentPhase].removeAtPhaseEnd.SetActive(false);

            if (phases[currentPhase].addAtPhaseEnd && phases[currentPhase].newSpawnPoint) {
                Instantiate(phases[currentPhase].addAtPhaseEnd, phases[currentPhase].newSpawnPoint.position,
                    phases[currentPhase].newSpawnPoint.rotation);
            }
            currentPhase++;
            bossAnim.SetInteger(Phase, currentPhase + 1);
            GameManager.instance.AddScore(phases[currentPhase].score);
        }
        else if (phases[currentPhase].phaseShot != null) {
            foreach (BattleShot shot in phases[currentPhase].phaseShot) {
                shot.shotCounter -= Time.deltaTime;
                if (shot.shotCounter <= 0) {
                    shot.shotCounter = shot.timeBetweenShots;
                    Instantiate(shot.shot, shot.firePoint.transform.position, shot.firePoint.transform.rotation);
                }
            }
        }
    }

    protected override IEnumerator EndBattle()
    {
        yield return base.EndBattle();
        bossAnim.enabled = false;
    }
}