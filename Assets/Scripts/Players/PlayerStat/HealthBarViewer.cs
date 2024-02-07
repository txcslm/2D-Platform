using UnityEngine;
using UnityEngine.UI;
using Vector2 = UnityEngine.Vector2;

namespace Players.PlayerStat
{
	public class HealthBarViewer : MonoBehaviour
	{
		private GameObject[] _heartContainers;
		private Image[] _heartFills;

		public Transform heartsParent;
		public GameObject heartContainerPrefab;

		private void Start()
		{
			_heartContainers = new GameObject[(int)PlayerStats.Instance.MaxTotalHealth];
			_heartFills = new Image[(int)PlayerStats.Instance.MaxTotalHealth];

			PlayerStats.Instance._onHealthChangedCallback += UpdateHeartsHUD;
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
			{
				if (i < PlayerStats.Instance.MaxHealth)
				{
					_heartContainers[i].SetActive(true);
				}
				else
				{
					_heartContainers[i].SetActive(false);
				}
			}
		}

		private void SetFilledHearts()
		{
			for (int i = 0; i < _heartFills.Length; i++)
			{
				if (i < PlayerStats.Instance.Health)
				{
					_heartFills[i].fillAmount = 1;
				}
				else
				{
					_heartFills[i].fillAmount = 0;
				}
			}

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
				GameObject temp = Instantiate(heartContainerPrefab, heartsParent, false);
				_heartContainers[i] = temp;
				Vector2 position = new Vector2(temp.transform.position.x + i, temp.transform.position.y);
				temp.transform.position = position;
				_heartFills[i] = temp.transform.Find("HeartFill").GetComponent<Image>();
			}
		}
	}
}