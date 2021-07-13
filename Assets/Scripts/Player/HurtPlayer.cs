﻿//
// © Arthur Vasseur arthurvasseur.fr
//

using UnityEngine;

namespace Player
{
    public class HurtPlayer : MonoBehaviour
    {
        private void OnCollisionEnter2D(Collision2D other)
        {
            if (other.gameObject.CompareTag("Player"))
                HealthManager.instance.HurtPlayer();
        }
    }
}
