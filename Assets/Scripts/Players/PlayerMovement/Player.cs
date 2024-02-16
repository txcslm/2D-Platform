using UnityEngine;

namespace Players.PlayerMovement
{
	public class Player : Character
	{
		private const string JumpInput = "Jump";
		private readonly static int Jumping = Animator.StringToHash(JumpInput);

		[SerializeField] private LayerMask _enemyAttackRange;

		private void Update()
		{
			HandleInput();
			FlipCharacter();

			if (IsDiyng())
			{
				Die();
			}
		}

		private void HandleInput()
		{
			float horizontalInput = Input.GetAxis(HorizontalInput);
			Move(horizontalInput);

			_animator.SetFloat(Speed, Mathf.Abs(horizontalInput));

			switch (_currentState)
			{
				case CharacterState.Running:
				{
					if (Input.GetButtonDown(JumpInput)) Jump();
					break;
				}
				case CharacterState.Jumping:
				{
					if (Input.GetButtonUp(JumpInput))
					{
						const float jumpVelocityReduction = 0.5f;
						Vector2 velocity = _rigidbody2D.velocity;
						velocity = new Vector2(velocity.x, velocity.y * jumpVelocityReduction);
						_rigidbody2D.velocity = velocity;
					}
					break;
				}
			}

			if (IsGrounded() == false ||
			    _currentState is not (CharacterState.Jumping or CharacterState.Idle or CharacterState.Running))
				return;
			
			_currentState = CharacterState.Running;
		}

		protected override void Die()
		{
			float delayToGameOver = 1f;
			
			base.Die();
			Invoke(nameof(GameOver), delayToGameOver);
		}

		private void GameOver() =>
			SceneReloader.ReloadScene();

		private void Jump()
		{
			if (IsGrounded() == false)
				return;
			
			_currentState = CharacterState.Jumping;

			_animator.SetTrigger(Jumping);
			_rigidbody2D.velocity = new Vector2(_rigidbody2D.velocity.x, jumpForce);
		}
	}
}