using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
	[SerializeField] private float _moveSpeed;

	[SerializeField] private Joystick _joystick;
	[SerializeField] private Button _bombButton;

	private Rigidbody2D _rigidbody;

	private Vector2Int _moveDirection;

	public event UnityAction<Vector2Int> BombButtonClicked;
	public event UnityAction<MoveState> MoveStateChanged;

	private void Awake()
	{
		_rigidbody = GetComponent<Rigidbody2D>();

		_bombButton.onClick.AddListener(() =>
		{
			BombButtonClicked?.Invoke(transform.localPosition.Vector3toVector2Int() + _moveDirection);
		});
	}

	private void FixedUpdate()
	{
		float vertical = _joystick.Vertical;
		float horizontal = _joystick.Horizontal;

		float absoluteVertical = Mathf.Abs(vertical);
		float absoluteHorizontal = Mathf.Abs(horizontal);

		if (absoluteVertical > 0.1 || absoluteHorizontal > 0.1)
		{
			_rigidbody.velocity = new Vector2(horizontal * _moveSpeed, vertical * _moveSpeed);

			if (absoluteVertical > absoluteHorizontal)
			{
				if (vertical > 0)
				{
					_moveDirection = Vector2Int.up;
					ChangeMoveState(MoveState.WalkUp);
				}
				else
				{
					_moveDirection = Vector2Int.down;
					ChangeMoveState(MoveState.WalkDown);
				}
			}
			else
			{
				if (horizontal > 0)
				{
					_moveDirection = Vector2Int.right;
					ChangeMoveState(MoveState.WalkRight);
				}
				else
				{
					_moveDirection = Vector2Int.left;
					ChangeMoveState(MoveState.WalkLeft);
				}
			}
		}
		else
		{
			_rigidbody.velocity = Vector2.zero;
			ChangeMoveState(MoveState.Idle);
		}
	}

	private void ChangeMoveState(MoveState state)
	{
		MoveStateChanged?.Invoke(state);
	}
}

public enum MoveState
{
	WalkLeft,
	WalkRight,
	WalkUp,
	WalkDown,
	Idle
}