using UnityEngine;
using UnityEngine.Events;

public class PlayerStats : MonoBehaviour
	{
		[SerializeField] private float _health;
		[SerializeField] private float _maxHealth;
		[SerializeField] private float _maxTotalHealth;
		
		public UnityAction HealthChangedEvent;

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
		
		public float Health => _health;
		public float MaxHealth => _maxHealth;
		public float MaxTotalHealth => _maxTotalHealth;

		private void Awake()
		{
			if (instance == null)
				instance = this;
			else
				Destroy(gameObject);
		}

		public void Heal(float health)
		{
			this._health += health;
			ClampHealth();
		}

		public void TakeDamage(float dmg)
		{
			_health -= dmg;
			ClampHealth();
		}

		public void AddHealth()
		{
			if (_maxHealth < _maxTotalHealth)
			{
				_maxHealth += 1;
				_health = _maxHealth;

				HealthChangedEvent?.Invoke();
			}
		}

		private void ClampHealth()
		{
			_health = Mathf.Clamp(_health, 0, _maxHealth);

			HealthChangedEvent?.Invoke();
		}
	}