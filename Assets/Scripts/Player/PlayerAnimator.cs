using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(PlayerController))]
[RequireComponent(typeof(PlayerRespawner))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerAnimator : MonoBehaviour
{
	[SerializeField] private int _protectionAnimationTicks;
	[SerializeField] private float _protectionAnimationTickDuration;

	private Animator _animator;
	private PlayerController _playerController;
	private SpriteRenderer _spriteRenderer;
	private PlayerRespawner _playerRespawner;

	private void Awake()
	{
		_animator = GetComponent<Animator>();
		_playerController = GetComponent<PlayerController>();
		_spriteRenderer = GetComponent<SpriteRenderer>();
		_playerRespawner = GetComponent<PlayerRespawner>();
	}

	private void OnEnable()
	{
		_playerController.MoveStateChanged += OnMoveStateChanged;
		_playerRespawner.PlayerRespawned += OnPlayerRespawned;
	}

	private void OnDisable()
	{
		_playerController.MoveStateChanged -= OnMoveStateChanged;
		_playerRespawner.PlayerRespawned -= OnPlayerRespawned;
	}

	private void OnPlayerRespawned()
	{
		StartCoroutine(RespawnAnimation());
	}

	private void OnMoveStateChanged(MoveState state)
	{
		_animator.Play(state.ToString());
	}

	private IEnumerator RespawnAnimation()
	{
		var wait = new WaitForSeconds(_protectionAnimationTickDuration);

		Color currentColor = _spriteRenderer.color;
		Color targetColor = new Color(1, 1, 1, 0);

		for (int i = 0; i < _protectionAnimationTicks; i++)
		{
			float elapsedTime = 0;

			while (elapsedTime <= _protectionAnimationTickDuration)
			{
				_spriteRenderer.color = Color.Lerp(currentColor, targetColor, elapsedTime / _protectionAnimationTickDuration);
				elapsedTime += Time.deltaTime;
				yield return null;
			}
		}

		_spriteRenderer.color = currentColor;
	}
}
