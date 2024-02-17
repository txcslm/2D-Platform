using EnemyMovement;
using Players.PlayerMovement;
using System.Collections;
using UnityEngine;

public class VampireRay : MonoBehaviour
{
    [SerializeField] private float _rayDistance = 10f;
    [SerializeField] private PlayerStats _playerStats;
    [SerializeField] private Player _player;
    [SerializeField] private LayerMask _enemyLayerMask;
    [SerializeField] private float _vampirismDuration = 6f;

    private bool _isVampirismActive = false;
    private float _vampirismStartTime;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && !_isVampirismActive)
        {
            StartCoroutine(CastVampireRay());
        }

        if (_isVampirismActive)
        {
            UpdateVampirism();
        }
    }

    private void UpdateVampirism()
    {
        if (Time.fixedTime - _vampirismStartTime >= _vampirismDuration)
        {
            _isVampirismActive = false;
        }
        else
        {
            CastRay(transform.right);

            if (_player.GetComponent<SpriteRenderer>().flipX)
                CastRay(-transform.right);
        }
    }

    private IEnumerator CastVampireRay()
    {
        _isVampirismActive = true;
        _vampirismStartTime = Time.fixedTime;

        while (_isVampirismActive)
        {
            yield return new WaitForFixedUpdate();
        }
    }

    private void CastRay(Vector2 direction)
    {
        Vector2 rayOrigin = transform.position;

        RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, _rayDistance, _enemyLayerMask);

        Debug.DrawRay(rayOrigin, direction * _rayDistance, Color.red, 1f);

        DamageAndHeal(hit);
    }

    private void DamageAndHeal(RaycastHit2D hit)
    {
        if (hit.collider != null)
        {
            if (hit.collider.TryGetComponent(out Enemy enemy))
            {
                enemy.TakeDamage(_player.Damage);

                if (_playerStats != null)
                    _playerStats.Heal(_player.Damage);
            }
        }
    }
}