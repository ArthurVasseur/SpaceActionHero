//
// © Arthur Vasseur arthurvasseur.fr
//
using UnityEngine;

public class LevelEnd : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
            StartCoroutine(GameManager.instance.EndLevel());
    }
}

