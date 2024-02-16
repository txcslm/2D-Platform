using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class HealthBarViewSlider : HealthView
{
	private const float MinValue = 0.01f;
	
	[SerializeField] private Slider _healthBar;
	[SerializeField] private float _smoothSpeed;
	
	private Coroutine _smoothUpdateCoroutine;
	
	private void Start()
	{
		_healthBar.maxValue = MaxHealth;
		_healthBar.value = CurrentHealth;
	}

	protected override void UpdateEnemyHealth(float targetValue)
	{

		if (_smoothUpdateCoroutine != null)
		{
			StopCoroutine(_smoothUpdateCoroutine);
		}

		_smoothUpdateCoroutine = StartCoroutine(SmoothUpdateCoroutine(targetValue));
	}
	
	private IEnumerator SmoothUpdateCoroutine(float targetValue)
	{
		while (Mathf.Abs(_healthBar.value - targetValue) > MinValue)
		{
			_healthBar.value = Mathf.MoveTowards(_healthBar.value, targetValue, Time.deltaTime * _smoothSpeed);
			yield return null;
		}

		_healthBar.value = targetValue;
		_smoothUpdateCoroutine = null;
	}
}