using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class PlayerRespawner : MonoBehaviour
{
	[SerializeField] private float _protectionDuration;

	private Vector2 _playerRespawnPosition;

	private bool _isProtected = false;

	public event UnityAction PlayerRespawned;

	private void Awake()
	{
		_playerRespawnPosition = transform.position;
	}

	private void OnTriggerEnter2D(Collider2D other)
	{
		if (other.TryGetComponent(out Explosion explosion) || other.TryGetComponent(out EnemyController enemy))
		{
			if (_isProtected == false)
			{
				PlayerRespawned?.Invoke();
				RespawnPlayer();
			}
		}
	}

	private void RespawnPlayer()
	{
		transform.position = _playerRespawnPosition;
		StartCoroutine(SetProtection());
	}

	private IEnumerator SetProtection()
	{
		_isProtected = true;

		var wait = new WaitForSeconds(_protectionDuration);

		yield return wait;

		_isProtected = false;
	}
}
