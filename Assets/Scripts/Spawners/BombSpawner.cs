using UnityEngine;
using UnityEngine.Events;

public class BombSpawner : MonoBehaviour
{
	[SerializeField] private Bomb _bombPrefab;
	[SerializeField] private Transform _container;

	public void SpawnBomb(Vector2 position, UnityAction onDestroyCallback)
	{
		Bomb bomb = Instantiate(_bombPrefab, _container);
		bomb.transform.localPosition = position;
		bomb.InitializeOnDestroyCallback(onDestroyCallback);
	}
}
