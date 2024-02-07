using Players.PlayerStat;
using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour
{
    protected const string HorizontalInput = "Horizontal";
    
    [SerializeField] public float damage;
    
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float jumpForce = 10f; 
    [SerializeField] protected float groundRaycastDistance = 0.1f;
    
    [SerializeField] private LayerMask groundLayerMask;

    protected readonly int Speed = Animator.StringToHash(nameof(Speed));
    protected readonly int Diyng = Animator.StringToHash(nameof(Diyng));

    protected Rigidbody2D _rigidbody2D;
    protected Animator _animator;
    protected CharacterState _currentState;
    protected PlayerStats _playerStats;

    protected enum CharacterState
    {
        Idle,
        Running,
        Die,
        Jumping
    }

    protected void Awake()
    {
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerStats = GetComponent<PlayerStats>();
        _currentState = CharacterState.Idle;
    }

    protected void Move(float horizontalInput) =>
        _rigidbody2D.velocity = new Vector2(horizontalInput * moveSpeed, _rigidbody2D.velocity.y);

    protected bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundRaycastDistance, groundLayerMask);
        return hit.collider != null;
    }

    protected virtual bool IsDiyng() =>
        _playerStats.Health == 0;

    protected virtual void Die()
    {
        _currentState = CharacterState.Die;
        _animator.SetTrigger(Diyng);
    }

    protected virtual void FlipCharacter()
    {
        Vector3 scale = transform.localScale;

        float horizontalInput = Input.GetAxis(HorizontalInput);

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
}