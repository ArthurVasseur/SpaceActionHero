//
// © Arthur Vasseur arthurvasseur.fr
//
using UnityEngine;

public class DestroyOverTime : MonoBehaviour
{
    public float lifeTime;
    private void Update()
    {
        Destroy(gameObject, lifeTime);
    }
}
