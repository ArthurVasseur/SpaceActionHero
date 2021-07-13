//
// © Arthur Vasseur arthurvasseur.fr
//

using Player;
using UnityEngine;

public class ShotFolowPlayer : MonoBehaviour
{
    [SerializeField][Tooltip("Missile speed")] private float speed = 4f;
    private GameObject player;
    private float timer = 0.35f;
    private Vector2 startPosition;
    private void Start()
    {
        player = GameObject.FindWithTag("Player");
        startPosition.x = transform.position.x;
        startPosition.y = transform.position.y;
    }
    private void Update()
    {
        if (timer >= 0) {
            speed = 1f;
            transform.position += new Vector3(startPosition.x * speed * Time.deltaTime, startPosition.y * speed * Time.deltaTime, 0f);
        }
        if (player && timer <= 0) {
            speed = 4f;
            transform.position = Vector3.MoveTowards(transform.position, player.transform.position, speed * Time.deltaTime);
            Vector3 dir = player.transform.position - transform.position;
            float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
            Quaternion rotation = Quaternion.Euler(new Vector3(0, 0, angle - 90f));
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 5f * Time.deltaTime);
        }
        timer -= Time.deltaTime;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            HealthManager.instance.HurtPlayer();
        Destroy(gameObject);
    }
}
