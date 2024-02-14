using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;

public class HealthBarViewer : MonoBehaviour
{
	[SerializeField] private Transform heartsParent;
	[SerializeField] private GameObject heartContainerPrefab;

	private GameObject[] heartContainers;
	private Image[] heartFills;

	private void Start()
	{
		heartContainers = new GameObject[(int)PlayerStats.Instance.MaxTotalHealth];
		heartFills = new Image[(int)PlayerStats.Instance.MaxTotalHealth];

		PlayerStats.Instance.OnHealthChanged += UpdateHeartsHUD;
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
		for (int i = 0; i < heartContainers.Length; i++)
			heartContainers[i].SetActive(i < PlayerStats.Instance.MaxHealth);
	}

	private void SetFilledHearts()
	{
		for (int i = 0; i < heartFills.Length; i++)
			heartFills[i].fillAmount = i < PlayerStats.Instance.Health ? 1 : 0;

		if (PlayerStats.Instance.Health % 1 != 0)
		{
			int lastPos = Mathf.FloorToInt(PlayerStats.Instance.Health);
			heartFills[lastPos].fillAmount = PlayerStats.Instance.Health % 1;
		}
	}

	private void InstantiateHeartContainers()
	{
		for (int i = 0; i < PlayerStats.Instance.MaxTotalHealth; i++)
		{
			GameObject temp = Instantiate(heartContainerPrefab, heartsParent, false);
			heartContainers[i] = temp;
			Vector2 position = new Vector2(temp.transform.position.x + i, temp.transform.position.y);
			temp.transform.position = position;
			heartFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>();
		}
	}
}