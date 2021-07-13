//
// © Arthur Vasseur arthurvasseur.fr
//
using UnityEngine;

public class MovingObject : MonoBehaviour
{
    [SerializeField]private float moveSpeed;
    [SerializeField]private bool canRotate;
    [SerializeField]private int scoreValue = 25;

    public int ScoreValue { get => scoreValue; set => scoreValue = value; }
    private void Update()
    {
        if (canRotate && transform.parent) {
            transform.RotateAround(transform.parent.position, new Vector3(0, 0, 1), 50 * Time.deltaTime);
            transform.rotation = new Quaternion(transform.rotation.x, transform.rotation.y, 0, 1);
        }
        else transform.position -= new Vector3(0f, moveSpeed * Time.deltaTime, 0f);
    }
    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
