using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public abstract class Character : MonoBehaviour
{
    protected const string HorizontalInput = "Horizontal";
    
    
    [SerializeField] protected float moveSpeed = 5f;
    [SerializeField] protected float jumpForce = 10f; 
    [SerializeField] protected float groundRaycastDistance = 0.1f;
    
    [SerializeField] private LayerMask _groundLayerMask;
    [field: SerializeField] public float Damage { get; private set; }

    protected readonly int Speed = Animator.StringToHash(nameof(Speed));
    protected readonly int Diyng = Animator.StringToHash(nameof(Diyng));

    private PlayerStats _playerStats;
    protected Rigidbody2D _rigidbody2D;
    protected Animator _animator;
    protected CharacterState _currentState;
    protected SpriteRenderer _spriteRenderer;

    protected enum CharacterState
    {
        Idle,
        Running,
        Die,
        Jumping
    }

    protected void Awake()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _rigidbody2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _playerStats = GetComponent<PlayerStats>();
        _currentState = CharacterState.Idle;
    }

    protected void Move(float horizontalInput)
    {
        _rigidbody2D.velocity = new Vector2(horizontalInput * moveSpeed, _rigidbody2D.velocity.y);
    }

    protected bool IsGrounded()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, groundRaycastDistance, _groundLayerMask);
        return hit.collider != null;
    }

    protected virtual bool IsDiyng()
    {
        return _playerStats.Health == 0;
    }
    
    protected virtual void Die()
    {
        _currentState = CharacterState.Die;
        _animator.SetTrigger(Diyng);
    }

    protected virtual void FlipCharacter()
    {
        float horizontalInput = Input.GetAxis(HorizontalInput);

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
}