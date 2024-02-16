using System;
using UnityEngine;

namespace FirstAid
{
	public class Heart : MonoBehaviour
	{
		private PlayerStats _playerStats;

		[SerializeField] private LayerMask _playerLayer;
		[SerializeField] private float _recoverableHealth;

		private void Start()
		{
			_playerStats = PlayerStats.Instance;

			if (_playerStats == null)
				throw new Exception("_playerStats is null");
		}

		private void TryDestroyObject()
		{
			Destroy(gameObject);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (_playerStats.Health == _playerStats.MaxTotalHealth)
				return;

			TryDestroyObject();

			if ((_playerLayer.value & 1 << other.gameObject.layer) != 0)
			{
				if (_playerStats.Health == _playerStats.MaxHealth)
				{
					_playerStats.AddHealth();
				}
				_playerStats.Heal(_recoverableHealth);
			}
		}
	}
}