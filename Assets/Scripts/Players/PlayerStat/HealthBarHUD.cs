using UnityEngine;

namespace Players.PlayerStat
{
    public class HealthBarHUD : MonoBehaviour
    {
        private void AddHealth()
        {
            PlayerStats.Instance.AddHealth();
        }

        private void Heal(float health)
        {
            PlayerStats.Instance.Heal(health);
        }

        private void Hurt(float dmg)
        {
            PlayerStats.Instance.TakeDamage(dmg);
        }
    }
}