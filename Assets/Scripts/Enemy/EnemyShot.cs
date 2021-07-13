//
// © Arthur Vasseur arthurvasseur.fr
//

using Player;
using UnityEngine;

public class EnemyShot : MonoBehaviour
{
    [SerializeField][Tooltip("Speed of shot")]private float shotSpeed = 7f;
    [SerializeField][Tooltip("When Collider2D is triggered instantiate impact effect")]private GameObject impactEffect;

    private void Update()
    {
        transform.position -= new Vector3(0f, shotSpeed * Time.deltaTime, 0f);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Instantiate(impactEffect, transform.position, transform.rotation);
        if (other.CompareTag("Player"))
            HealthManager.instance.HurtPlayer();
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
