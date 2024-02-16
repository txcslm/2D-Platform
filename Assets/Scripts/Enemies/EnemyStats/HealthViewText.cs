using TMPro;
using UnityEngine;

public class HealthViewText : HealthView
{
	[SerializeField] private TextMeshProUGUI _healthText;

	protected override void UpdateEnemyHealth(float targetValue) =>
		_healthText.text = $"{targetValue}/{MaxHealth}";
}