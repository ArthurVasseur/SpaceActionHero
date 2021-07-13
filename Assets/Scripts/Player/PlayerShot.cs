//
// © Arthur Vasseur arthurvasseur.fr
//

using UnityEngine;

namespace Player
{
    public class PlayerShot : MonoBehaviour
    {
        [SerializeField][Tooltip("Speed of shot")]private float shotSpeed = 7f;
        [SerializeField][Tooltip("When Collider2D is triggered instantiate impact effect")]private GameObject impactEffect;
        [SerializeField][Tooltip("Explosion effect if collider is spaceObject")]private GameObject explosionEffect;
   
        private GameObject impact;
        private AudioSource shotAudio;
        private bool canBeDestroyed;

        private void Awake()
        {
            shotAudio = GetComponent<AudioSource>();
        }

        private void Update()
        {
            transform.position += new Vector3(0f, shotSpeed * Time.deltaTime, 0f);
            if (!shotAudio.isPlaying && canBeDestroyed)
                Destroy(gameObject);
            if (canBeDestroyed) {
                GetComponent<SpriteRenderer>().enabled = false;
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.CompareTag("PickUp") && !other.CompareTag("Player"))
                Instantiate(impactEffect, other.transform.position, other.transform.rotation,other.transform);
            if (other.CompareTag("SpaceObject")) {
                Instantiate(explosionEffect, other.transform.position, other.transform.rotation);
                Destroy(other.gameObject);
                GameManager.instance.AddScore(other.gameObject.GetComponent<MovingObject>().ScoreValue);
                canBeDestroyed = true;
            } else if (other.CompareTag("Enemy")) {
                if (other.GetComponent<EnemyController>()) 
                    other.GetComponent<EnemyController>().HurtEnemy();
                canBeDestroyed = true;
            } else if (other.CompareTag("Boss")) {
                BossManager.instance.HurtBoss();
                canBeDestroyed = true;
            } else if (other.CompareTag("BossMinion")) {
                other.GetComponent<BossMinion>().HurtMinion();
                canBeDestroyed = true;
            } else if (other.CompareTag("Missile"))
            {
                Debug.Log("Missile");
                Destroy(other.gameObject);
                canBeDestroyed = true;
            }
        }
        private void OnBecameInvisible()
        {
            Destroy(gameObject);
        }
    }
}
