using EnemyMovement;
using Players.PlayerMovement;
using UnityEngine;
using UnityEngine.Serialization;

public class AggroArea : MonoBehaviour
{
	[SerializeField] private Enemy _enemy;
	[SerializeField] private Transform _defaultTarget;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent(out Player player))
			_enemy.SetTarget(player.transform);
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.GetComponent<Player>() != null)
			_enemy.SetTarget(_defaultTarget);
	}
}