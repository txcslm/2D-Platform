using Players.PlayerMovement;
using UnityEngine;

[RequireComponent(typeof(ScoreCounter))]
public class Coin : MonoBehaviour
{
	[SerializeField] private int scoreValue = 5;
	[SerializeField] private ScoreCounter scoreCounter;
	[SerializeField] private Player _player;

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (_player != null)
		{
			Destroy(gameObject);

			if (scoreCounter != null)
				scoreCounter.IncreaseScore(scoreValue);
		}
	}
}