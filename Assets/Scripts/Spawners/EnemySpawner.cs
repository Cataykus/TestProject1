using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
	[SerializeField] private Transform _container;

	[SerializeField] private GameObject _enemyPrefab;

	[SerializeField] private float _enemySpawnCooldown;

	[SerializeField] private List<Transform> _enemySpawnPositions;

	private float _elapsedTime = 0;

	private void Start()
	{
		SpawnEnemy();
	}

	private void Update()
	{
		if (_elapsedTime > _enemySpawnCooldown)
		{
			SpawnEnemy();
			_elapsedTime = 0;
		}

		_elapsedTime += Time.deltaTime;
	}

	private void SpawnEnemy()
	{
		Vector3 spawnPosition = _enemySpawnPositions[Random.Range(0, _enemySpawnPositions.Count)].position;

		Instantiate(_enemyPrefab, spawnPosition, Quaternion.identity, _container);
	}
}
