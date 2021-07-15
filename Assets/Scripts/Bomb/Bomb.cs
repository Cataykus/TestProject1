using UnityEngine;
using UnityEngine.Events;
using System.Collections;

[RequireComponent(typeof(SpriteRenderer))]
public class Bomb : MonoBehaviour
{
	[SerializeField] private int _bombTicks;
	[SerializeField] private float _tickDuration;

	private SpriteRenderer _spriteRenderer;

	private UnityAction _onDestroyCallback;

	private void Awake()
	{
		_spriteRenderer = GetComponent<SpriteRenderer>();
	}

	public void InitializeOnDestroyCallback(UnityAction callback)
	{
		_onDestroyCallback = callback;
		StartCoroutine(StartTicks());
	}

	private IEnumerator StartTicks()
	{
		var wait = new WaitForSeconds(_tickDuration);

		Color currentColor = _spriteRenderer.color;
		Color targetColor = new Color(1, 1, 1, 0);

		for (int i = 0; i < _bombTicks; i++)
		{
			float elapsedTime = 0;

			while (elapsedTime <= _tickDuration)
			{
				_spriteRenderer.color = Color.Lerp(currentColor, targetColor, elapsedTime / _tickDuration);
				elapsedTime += Time.deltaTime;
				yield return null;
			}
		}

		transform.GetChild(0).gameObject.SetActive(true);

		yield return new WaitForSeconds(0.1f);

		_onDestroyCallback?.Invoke();
		Destroy(gameObject);
	}
}
