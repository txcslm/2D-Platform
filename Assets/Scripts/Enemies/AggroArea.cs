using EnemyMovement;
using Players.PlayerMovement;
using UnityEngine;
using UnityEngine.Serialization;

public class AggroArea : MonoBehaviour
{
	[SerializeField] private Enemy enemy;
	[SerializeField] private Transform defaultTarget;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent(out Player player))
			enemy.SetTarget(player.transform);
	}

	private void OnTriggerExit2D(Collider2D other)
	{
		if (other.GetComponent<Player>() != null)
			enemy.SetTarget(defaultTarget);
	}
}