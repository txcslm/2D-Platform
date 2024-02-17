using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	[field: SerializeField] public float CurrentHealth { get; private set; }
	[field: SerializeField] public float MaxHealth { get; private set; }
	
	private const float MinHealth = 0f;
	
	public event Action<float> ValueChanged;

	private void Awake() =>
		CurrentHealth = MaxHealth;

	public void TakeDamage(float damage)
	{
		CurrentHealth -= damage;
		
		CurrentHealth = Mathf.Clamp(CurrentHealth, MinHealth, MaxHealth);
		ValueChanged?.Invoke(CurrentHealth);
	}

	public void Heal(float heal)
	{
		CurrentHealth += heal;
		
		CurrentHealth = Mathf.Clamp(CurrentHealth, MinHealth, MaxHealth);
		ValueChanged?.Invoke(CurrentHealth);
	}
}