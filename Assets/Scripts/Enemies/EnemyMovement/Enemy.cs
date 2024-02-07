using Players.PlayerMovement;
using UnityEngine;

namespace EnemyMovement
{
    [RequireComponent(typeof(Transform))]
    public class Enemy : Character
    {
        private const float StoppingDistance = 0.1f;

        [SerializeField] private Transform targetA;
        [SerializeField] private Transform targetB;
        [SerializeField] private float _enemyHealth;
        [SerializeField] private LayerMask _hitBox;

        private Transform _target;
        private bool _isAggro = false;

        private void Start()
        {
            SetTarget(targetA);

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

        private void DestroyEnemy()
        {
            Destroy(gameObject);
        }

        protected override void Die()
        {
            float delayToDestroy = 1f;
            
            base.Die();
            Invoke(nameof(DestroyEnemy), delayToDestroy);
        }

        protected override bool IsDiyng()
        {
            return _enemyHealth == 0;
        }

        protected override void FlipCharacter()
        {
            Vector3 scale = transform.localScale;
            float horizontalInput = _rigidbody2D.velocity.x;

            switch (horizontalInput)
            {
                case > 0:
                    scale.x = Mathf.Abs(scale.x);
                    break;
                case < 0:
                    scale.x = -Mathf.Abs(scale.x);
                    break;
            }

            transform.localScale = scale;
        }

        private void Patrol()
        {
            SetTarget(targetA);
            MoveToTarget();
            
            if (Vector2.Distance(transform.position, targetA.position) < StoppingDistance)
                SetTarget(targetB);
            else if (Vector2.Distance(transform.position, targetB.position) < StoppingDistance)
                SetTarget(targetA);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.transform != _target) 
                return;
            
            if (other.TryGetComponent(out Player player))
                TakeDamage(player.damage);
            
            SetTarget(_target == targetA ? targetB : targetA);
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

        private void TakeDamage(float playerDamage)
        {
            _enemyHealth -= playerDamage;
        }
    }
}