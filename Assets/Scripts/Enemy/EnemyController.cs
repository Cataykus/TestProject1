using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EnemyController : MonoBehaviour
{
	[SerializeField] private float _moveSpeed;

	private Grid _grid;

	private Vector2 movePosition;

	public event UnityAction<MoveState> MoveStateChanged;

	private void Start()
	{
		_grid = FindObjectOfType<Grid>();
		StartMoving();
	}

	private void StartMoving()
	{
		movePosition = _grid.GetEnemyRandomMovePosition(transform.localPosition.Vector3toVector2Int());

		Vector2 moveDirection = movePosition - transform.localPosition.Vector3toVector2();

		if (moveDirection == Vector2.left)
		{
			MoveStateChanged?.Invoke(MoveState.WalkRight);
		}

		if (moveDirection == Vector2.right)
		{
			MoveStateChanged?.Invoke(MoveState.WalkLeft);
		}

		if (moveDirection == Vector2.up)
		{
			MoveStateChanged?.Invoke(MoveState.WalkUp);
		}

		if (moveDirection == Vector2.down)
		{
			MoveStateChanged?.Invoke(MoveState.WalkDown);
		}

		StartCoroutine(MovingRoutine());
	}

	private IEnumerator MovingRoutine()
	{
		var wait = new WaitForEndOfFrame();

		while (Vector2.Distance(transform.localPosition, movePosition) > 0)
		{
			transform.localPosition = Vector2.MoveTowards(transform.localPosition, movePosition, _moveSpeed * Time.deltaTime);
			yield return wait;
		}

		StartMoving();
	}
}
