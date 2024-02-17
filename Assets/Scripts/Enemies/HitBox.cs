using Players.PlayerMovement;
using UnityEngine;

public class HitBox : MonoBehaviour
{
	[SerializeField] private EnemyHealth _enemyHealth;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent(out Player player))
			_enemyHealth.TakeDamage(player.Damage);
	}
}