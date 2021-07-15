using UnityEngine;

public class GridCell : MonoBehaviour
{
	[SerializeField] private GridCellType _cellType;

	public GridCellType CellType => _cellType;

	public int X => ((int)transform.localPosition.x);
	public int Y => ((int)transform.localPosition.y);

	public void SetWithBomb()
	{
		_cellType = GridCellType.WithBomb;
	}

	public void SetEmpty()
	{
		_cellType = GridCellType.Empty;
	}
}

public enum GridCellType
{
	Empty,
	Wall,
	WithBomb
}
