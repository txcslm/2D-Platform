using EnemyMovement;
using UnityEngine;

public class EnemyAttackRange : MonoBehaviour
{
	[SerializeField] private PlayerStats _playerStats;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent(out Enemy enemy))
			_playerStats.TakeDamage(enemy.Damage);
	}
}