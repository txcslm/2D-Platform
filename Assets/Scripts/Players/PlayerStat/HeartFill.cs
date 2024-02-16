using UnityEngine;
using UnityEngine.UI;

public class HeartFill : MonoBehaviour
{
	private Image _fillImage;

	private void Awake()
	{
		_fillImage = GetComponent<Image>();
	}

	public void SetFillAmount(float amount)
	{
		_fillImage.fillAmount = amount;
	}

	public float FillAmount
	{
		get { return _fillImage.fillAmount; }
		set { _fillImage.fillAmount = value; }
	}
}