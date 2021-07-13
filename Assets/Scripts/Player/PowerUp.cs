//
// © Arthur Vasseur arthurvasseur.fr
//

using UnityEngine;

namespace Player
{
    public class PowerUp : MonoBehaviour
    {
        [SerializeField]private bool isShield;
        [SerializeField]private bool isBoost;
        [SerializeField]private bool isDoubleShot;
        [SerializeField] private bool isHealth;

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) {
                Destroy(gameObject);
                if (isShield)
            
                    HealthManager.instance.ActivateShield();
                else if (isBoost)
                    PlayerController.instance.ActivateSpeedBoost();
                else if (isDoubleShot)
                {
                    if (PlayerController.instance.DoubleShotActive)
                        GameManager.instance.AddScore(300);
                    else PlayerController.instance.DoubleShotActive = true;
                } else if (isHealth)
                    HealthManager.instance.CurrentHealth++;
            }
        }
    }
}
