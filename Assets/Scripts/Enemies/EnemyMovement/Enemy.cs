using Players.PlayerMovement;
using UnityEngine;

namespace EnemyMovement
{
	[RequireComponent(typeof(Transform))]
	public class Enemy : Character
	{
		private const float StoppingDistance = 0.1f;

		[SerializeField] private Transform _targetA;
		[SerializeField] private Transform _targetB;
		[SerializeField] private EnemyHealth _enemyHealthView;
		[SerializeField] private LayerMask _hitBox;

		private Transform _target;
		private bool _isAggro = false;

		private void Start()
		{
			SetTarget(_targetA);

			if (_rigidbody2D == null || _animator == null)
			{
				Debug.LogError("Rigidbody2D or Animator is not assigned!");
				enabled = false;
			}
		}

		private void Update()
		{
			if (_isAggro == false)
			{
				MoveToTarget();
				UpdateEnemyState();
				FlipCharacter();

				if (IsDiyng())
					Die();
			}
			else
			{
				Patrol();
			}
		}

		public void SetTarget(Transform target)
		{
			_target = target;
		}

		public void TakeDamage(float damage)
		{
			_enemyHealthView.TakeDamage(damage);
		}

		protected override void Die()
		{
			float delayToDestroy = 1f;

			base.Die();
			Invoke(nameof(DestroyEnemy), delayToDestroy);
		}

		protected override bool IsDiyng()
		{
			return _enemyHealthView.CurrentHealth == 0;
		}

		protected override void FlipCharacter()
		{
			float horizontalInput = _rigidbody2D.velocity.x;

			switch (horizontalInput)
			{
				case > 0:
					_spriteRenderer.flipX = false;
					break;
				case < 0:
					_spriteRenderer.flipX = true;
					break;
			}
		}
		
		private void DestroyEnemy()
		{
			Destroy(gameObject);
		}
		
		private void Patrol()
		{
			SetTarget(_targetA);
			MoveToTarget();

			if (Vector2.Distance(transform.position, _targetA.position) < StoppingDistance)
				SetTarget(_targetB);
			else if (Vector2.Distance(transform.position, _targetB.position) < StoppingDistance)
				SetTarget(_targetA);
		}

		private void OnTriggerEnter2D(Collider2D other)
		{
			if (other.transform != _target)
				return;
			
			SetTarget(_target == _targetA ? _targetB : _targetA);
		}

		private void MoveToTarget()
		{
			if (_target == null)
				return;

			Vector2 direction = (_target.position - transform.position).normalized;
			_rigidbody2D.velocity = new Vector2(direction.x * moveSpeed, _rigidbody2D.velocity.y);
		}

		private void UpdateEnemyState()
		{
			if (_rigidbody2D == null)
				return;

			_currentState = _rigidbody2D.velocity.magnitude > 0 ? CharacterState.Running : CharacterState.Idle;

			if (_animator != null)
				_animator.SetFloat(Speed, _rigidbody2D.velocity.magnitude);
		}
	}
}