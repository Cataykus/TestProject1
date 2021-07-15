using UnityEngine;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(EnemyController))]
public class EnemyAnimator : MonoBehaviour
{
	private Animator _animator;
	private EnemyController _enemyController;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_enemyController = GetComponent<EnemyController>();
	}

	private void OnEnable()
	{
		_enemyController.MoveStateChanged += OnMoveStateChanged;
	}

	private void OnDisable()
	{
		_enemyController.MoveStateChanged -= OnMoveStateChanged;
	}

	private void OnMoveStateChanged(MoveState state)
	{
		_animator.Play(state.ToString());
	}
}
