//
// © Arthur Vasseur arthurvasseur.fr
//
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    [Tooltip("Apply damage to parent when Enemy is dead")]public bool hurtParent;
    [SerializeField][Tooltip("Move speed of gameObject")]private float moveSpeed;
    [SerializeField][Tooltip("Shot to Fire")] private GameObject shotToFire;
    [SerializeField][Tooltip("Position to instantiate ShotToFire")]private Transform[] firePoints;
    [SerializeField][Tooltip("Time between shots")] private float timeBetweenShots;
    [SerializeField][Tooltip("Allow to shoot")]private bool canShoot;
    [SerializeField][Tooltip("Current heath of BossMinion")]private int currentHealth;
    [SerializeField][Tooltip("Instantiate when BossMinion is dead")]private GameObject deathEffect;
    [SerializeField][Tooltip("Score value to add to score")]private int scoreValue = 100;
    [SerializeField][Tooltip("Instantiate bonus when enemy is dead")]private GameObject[] powerUps;
    [SerializeField][Tooltip("PowerUp drop rate ")]private int dropSuccessRate = 15;
    [SerializeField][Tooltip("Can rotate around parent ?")]private bool canRotate;
    [SerializeField][Tooltip("Disable all movement")]private bool disableMovement;
    
    private bool allowShooting;
    private float shotCounter;

    private void Start()
    {
        shotCounter = timeBetweenShots;
    }

    private void Update()
    {
        if (transform.parent != null && canRotate) {
            transform.RotateAround(transform.parent.position, new Vector3(0, 0, 1), 50 * Time.deltaTime);
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, 1);
        }

        if (!disableMovement)
            transform.position += new Vector3(0f, -(moveSpeed * Time.deltaTime), 0f);

        if (allowShooting) {
            shotCounter -= Time.deltaTime;
            if (shotCounter <= 0) {
                shotCounter = timeBetweenShots;
                foreach (var firePoint in firePoints)
                    Instantiate(shotToFire, firePoint.position, firePoint.rotation);
            }
        }
    }

    public void HurtEnemy()
    {
        currentHealth--;
        if (currentHealth <= 0)
        {
            if (GameManager.instance)
                GameManager.instance.AddScore(scoreValue);

            Destroy(gameObject);
            Instantiate(deathEffect, transform.position, transform.rotation);
            if (gameObject.transform.parent != null && gameObject.transform.parent.gameObject.CompareTag("Enemy") && hurtParent) {
                if (transform.parent.gameObject.GetComponent<EnemyController>())
                    transform.parent.gameObject.GetComponent<EnemyController>().HurtEnemy();
            }

            if (Random.Range(0, 100) < dropSuccessRate && powerUps.Length > 0)
                Instantiate(powerUps[Random.Range(0, powerUps.Length)], transform.position, transform.rotation);
        }
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }

    private void OnBecameVisible()
    {
        if (canShoot)
            allowShooting = true;
    }
}
