using Players.PlayerMovement;
using UnityEngine;

[RequireComponent(typeof(ScoreCounter))]
public class Coin : MonoBehaviour
{
	[SerializeField] private int _scoreValue = 5;
	[SerializeField] private ScoreCounter _scoreCounter;

	private void OnTriggerEnter2D(Collider2D other)
	{
		other.TryGetComponent(out Player player);

		if (player != null)
		{
			Destroy(gameObject);

			if (_scoreCounter != null)
				_scoreCounter.IncreaseScore(_scoreValue);
		}
	}
}