using UnityEngine;
using System.Collections.Generic;

public class Grid : MonoBehaviour
{
	[SerializeField] private Transform _gridContainer;

	[SerializeField] private PlayerController _playerController;
	[SerializeField] private BombSpawner _bombSpawner;

	[SerializeField] private Vector2Int _gridSize;

	private GridCell[,] _grid;

	private void Awake()
	{
		InitializeGrid();
	}

	private void OnEnable()
	{
		_playerController.BombButtonClicked += OnBombButtonClicked;
	}

	private void OnDisable()
	{
		_playerController.BombButtonClicked -= OnBombButtonClicked;
	}

	private void OnBombButtonClicked(Vector2Int position)
	{
		if (position.x >= 0 && position.x < _gridSize.x && position.y >= 0 && position.y < _gridSize.y)
		{
			GridCell cell = _grid[position.x, position.y];
			if (cell.CellType == GridCellType.Empty)
			{
				cell.SetWithBomb();
				_bombSpawner.SpawnBomb(position, () =>
				{
					cell.SetEmpty();
				});
			}
		}
	}

	private void InitializeGrid()
	{
		_grid = new GridCell[_gridSize.x, _gridSize.y];

		for (int i = 0; i < _gridContainer.childCount; i++)
		{
			GridCell cell = _gridContainer.GetChild(i).GetComponent<GridCell>();
			_grid[cell.X, cell.Y] = cell;
		}
	}

	private bool CheckCellIsEmpty(Vector2Int position)
	{
		if (_grid[position.x, position.y].CellType == GridCellType.Empty)
		{
			return true;
		}
		else
		{
			return false;
		}
	}

	public Vector2Int GetEnemyRandomMovePosition(Vector2Int position)
	{
		List<Vector2Int> positions = new List<Vector2Int>();

		Vector2Int newPosition = position + Vector2Int.up;

		if (newPosition.y < _gridSize.y && newPosition.y >= 0)
		{
			if (CheckCellIsEmpty(newPosition))
			{
				positions.Add(newPosition);
			}
		}

		newPosition = position + Vector2Int.down;

		if (newPosition.y < _gridSize.y && newPosition.y >= 0)
		{
			if (CheckCellIsEmpty(newPosition))
			{
				positions.Add(newPosition);
			}

		}

		newPosition = position + Vector2Int.left;

		if (newPosition.x < _gridSize.x && newPosition.x >= 0)
		{
			if (CheckCellIsEmpty(newPosition))
			{
				positions.Add(newPosition);
			}
		}

		newPosition = position + Vector2Int.right;

		if (newPosition.x < _gridSize.x && newPosition.x >= 0)
		{
			if (CheckCellIsEmpty(newPosition))
			{
				positions.Add(newPosition);
			}
		}

		if (positions.Count > 0)
		{
			return positions[Random.Range(0, positions.Count)];
		}
		else
		{
			return Vector2Int.zero;
		}
	}
}