using Players.PlayerMovement;
using UnityEngine;

[RequireComponent(typeof(ScoreCounter))]
public class Coin : MonoBehaviour
{
	[SerializeField] private int scoreValue = 5;
	[SerializeField] private ScoreCounter scoreCounter;

	private void OnTriggerEnter2D(Collider2D other)
	{
		Player player = other.gameObject.GetComponent<Player>();

		if (player != null)
		{
			Destroy(gameObject);

			if (scoreCounter != null)
				scoreCounter.IncreaseScore(scoreValue);
		}
	}
}