using UnityEngine;
using UnityEngine.Events;

	public class PlayerStats : MonoBehaviour
	{
		[SerializeField] private float health;
		[SerializeField] private float maxHealth;
		[SerializeField] private float maxTotalHealth;
		
		public UnityAction OnHealthChanged;

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
		
		public float Health => health;
		public float MaxHealth => maxHealth;
		public float MaxTotalHealth => maxTotalHealth;

		private void Awake()
		{
			if (instance == null)
				instance = this;
			else
				Destroy(gameObject);
		}

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

				OnHealthChanged?.Invoke();
			}
		}

		private void ClampHealth()
		{
			health = Mathf.Clamp(health, 0, maxHealth);

			OnHealthChanged?.Invoke();
		}
	}