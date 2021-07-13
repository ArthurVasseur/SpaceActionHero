//
// © Arthur Vasseur arthurvasseur.fr
//

using UI;
using UnityEngine;

namespace Player
{
    public class HealthManager : MonoBehaviour
    {
        [Tooltip("Instance")]public static HealthManager instance;
    
        [SerializeField][Tooltip("Max player heath")]private int maxHeath;
        [SerializeField][Tooltip("Instantiate when player dead")]private GameObject deathEffect;
        [SerializeField][Tooltip("Max shield power")] private int shiedlMaxPower = 1;
        [SerializeField][Tooltip("Shield")]private GameObject shield;

        private int shieldPwr;
        private int currentHealth;
        public int CurrentHealth {
            get => currentHealth;
            set
            {
                currentHealth = value;
                UiManager.instance.LivesText = currentHealth.ToString();
            }
        }

        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
#if UNITY_EDITOR
            maxHeath = 9999;
#endif
            CurrentHealth = maxHeath;
        }
        public void HurtPlayer()
        {
            if (shield.activeInHierarchy) {
                shieldPwr--;
                if (shieldPwr <= 0)
                    shield.SetActive(false);
            }
            else
            {
                CurrentHealth -= 1;
                if (currentHealth <= 0) {
                    gameObject.SetActive(false);
                    GameManager.instance.KillPlayer();
                }
                PlayerController.instance.DoubleShotActive = false;
                Instantiate(deathEffect, transform.position, transform.rotation);
            }
        }

        public void ActivateShield()
        {
            if (shield.activeSelf)
                GameManager.instance.AddScore(300);
            else {
                shield.SetActive(true);
                shield.transform.position.Set(shield.transform.position.x, transform.position.y, 0.5f);
                shieldPwr = shiedlMaxPower;
            }
        }
    }
}