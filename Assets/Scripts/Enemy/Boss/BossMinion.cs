//
// © Arthur Vasseur arthurvasseur.fr
//
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMinion : MonoBehaviour
{
    [SerializeField][Tooltip("Current heath of BossMinion")]private int currentHealth;
    [SerializeField][Tooltip("Time between shots")]private float timeBetweenShots;
    [SerializeField][Tooltip("Score value to add to score")]private int scoreValue;
    [SerializeField][Tooltip("Instantiate when BossMinion is dead")]private GameObject deathEffect;
    [SerializeField][Tooltip("Shot to Fire")]private GameObject shotToFire;
    [SerializeField][Tooltip("When BossMinion is dead the sprite change ")]private Sprite squidDead;
    [SerializeField][Tooltip("Position to instantiate ShotToFire")]private Transform[] firePoints;
    
    private bool isDead;
    private float shotCounter;
    private void Start()
    {
        shotCounter = timeBetweenShots;
    }

    private void Update()
    {
        if (transform.parent) {
            transform.RotateAround(transform.parent.position, new Vector3(0, 0, 1), 50 * Time.deltaTime);
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, 1);
        }

        if (isDead) return;
        shotCounter -= Time.deltaTime;
        if (!(shotCounter <= 0)) return;
        shotCounter = timeBetweenShots;
        foreach (Transform firePoint in firePoints)
            Instantiate(shotToFire, firePoint.position, firePoint.rotation);
    }

    public void HurtMinion()
    {
        currentHealth--;
        if (currentHealth <= 0 && !isDead) {
            if (GameManager.instance)
                GameManager.instance.AddScore(scoreValue);
            isDead = true;
            Instantiate(deathEffect, transform.position, transform.rotation,transform);
            GetComponent<SpriteRenderer>().sprite = squidDead;
            transform.parent.gameObject.GetComponent<BossManagerPart2>().HurtBoss(5);
        }
    }

    public static IEnumerator DestroyMinion(IEnumerable<GameObject> minion, GameObject _explosion)
    {
        foreach (GameObject squid in minion) {
            GameObject explosion = Instantiate(_explosion, squid.transform.position, Quaternion.identity);
            explosion.transform.parent = squid.gameObject.transform;
            squid.GetComponent<SpriteRenderer>().enabled = false;
            squid.GetComponent<CircleCollider2D>().enabled = false;
            yield return new WaitForSeconds(0.5f);
            Destroy(squid);
        }
    }
}