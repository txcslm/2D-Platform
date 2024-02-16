using UnityEngine;

public abstract class HealthView : MonoBehaviour
{
	[SerializeField] private EnemyHealth _enemyHealth;

	protected float MaxHealth => _enemyHealth.MaxHealth;
	protected float CurrentHealth => _enemyHealth.CurrentHealth;
	
	private void Awake() =>
		_enemyHealth.ValueChanged += UpdateEnemyHealth;

	protected abstract void UpdateEnemyHealth(float targetValue);
}