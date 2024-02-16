using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;

public class HealthBarViewer : MonoBehaviour
{
	[SerializeField] private Transform _heartsParent;
	[SerializeField] private GameObject _heartContainerPrefab;

	private GameObject[] _heartContainers;
	private Image[] _heartFills;

	private void Start()
	{
		_heartContainers = new GameObject[(int)PlayerStats.Instance.MaxTotalHealth];
		_heartFills = new Image[(int)PlayerStats.Instance.MaxTotalHealth];

		PlayerStats.Instance.HealthChangedEvent += UpdateHeartsHUD;
		InstantiateHeartContainers();
		UpdateHeartsHUD();
	}

	private void UpdateHeartsHUD()
	{
		SetHeartContainers();
		SetFilledHearts();
	}

	private void SetHeartContainers()
	{
		for (int i = 0; i < _heartContainers.Length; i++)
			_heartContainers[i].SetActive(i < PlayerStats.Instance.MaxHealth);
	}

	private void SetFilledHearts()
	{
		for (int i = 0; i < _heartFills.Length; i++)
			_heartFills[i].fillAmount = i < PlayerStats.Instance.Health ? 1 : 0;

		if (PlayerStats.Instance.Health % 1 != 0)
		{
			int lastPos = Mathf.FloorToInt(PlayerStats.Instance.Health);
			_heartFills[lastPos].fillAmount = PlayerStats.Instance.Health % 1;
		}
	}

	private void InstantiateHeartContainers()
	{
		for (int i = 0; i < PlayerStats.Instance.MaxTotalHealth; i++)
		{
			GameObject temp = Instantiate(_heartContainerPrefab, _heartsParent, false);
			_heartContainers[i] = temp;
			Vector2 position = new Vector2(temp.transform.position.x + i, temp.transform.position.y);
			temp.transform.position = position;
			_heartFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>();
		}
	}
}