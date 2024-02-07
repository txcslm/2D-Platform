using UnityEngine;
namespace Players.PlayerStat
{
	public class PlayerStats : MonoBehaviour
	{
		public delegate void OnHealthChangedDelegate();

		public OnHealthChangedDelegate _onHealthChangedCallback;

		private static PlayerStats instance;

		public static PlayerStats Instance
		{
			get
			{
				if (instance == null)
					instance = FindObjectOfType<PlayerStats>();

				return instance;
			}
		}

		private void Awake()
		{
			if (instance == null)
				instance = this;
			else
				Destroy(gameObject);
		}

		[SerializeField] private float health;
		[SerializeField] private float maxHealth;
		[SerializeField] private float maxTotalHealth;

		public float Health => health;

		public float MaxHealth => maxHealth;

		public float MaxTotalHealth => maxTotalHealth;

		public void Heal(float health)
		{
			this.health += health;
			ClampHealth();
		}

		public void TakeDamage(float dmg)
		{
			health -= dmg;
			ClampHealth();
		}

		public void AddHealth()
		{
			if (maxHealth < maxTotalHealth)
			{
				maxHealth += 1;
				health = maxHealth;

				_onHealthChangedCallback?.Invoke();
			}
		}

		private void ClampHealth()
		{
			health = Mathf.Clamp(health, 0, maxHealth);

			_onHealthChangedCallback?.Invoke();
		}
	}
}