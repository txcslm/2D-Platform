using TMPro;
using UnityEngine;

public class ScoreCounter : MonoBehaviour
{
	private int _score;
	private TextMeshProUGUI _scoreText;

	private void Awake()
	{
		_scoreText = GetComponentInChildren<TextMeshProUGUI>();
	}

	public void IncreaseScore(int amount)
	{
		_score += amount;
		UpdateScoreText();
	}

	private void UpdateScoreText()
	{
		if (_scoreText != null)
		{
			_scoreText.text = $"Score: {_score}";
		}
	}
}